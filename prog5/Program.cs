using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace prog5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var googleMapsServices = new GoogleMapsServices("use_your_google_api_here");
            System.Console.Write("Digite um endereço: ");
            var address = Console.ReadLine();
            if (string.IsNullOrEmpty(address))
            {
                address = "Rua Simeão Varela de Sá, 03, Guarapuava, PR";
            }
            try
            {
                var location = await googleMapsServices.GetFirstLocation(address);
                System.Console.WriteLine(location);
                dynamic mapsobjdyn = await googleMapsServices.GetGeocodeDynamic(address);
                System.Console.WriteLine($"Endereço formatado: {mapsobjdyn.results[0].formatted_address}");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            float lat = -25.384997f;
            System.Console.Write("Digite a latitude: ");
            float.TryParse(Console.ReadLine(), out lat);
            float lng = -51.489275f;
            System.Console.Write("Digite a longitude: ");
            float.TryParse(Console.ReadLine(), out lng);
            try
            {
                var geocodeAddress = await googleMapsServices.GetAddress(lat, lng);
                System.Console.WriteLine($"Endereço formatado: {geocodeAddress}");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }

    public interface IGoogleMapsApi
    {
        [Get("/geocode/json?address={address}&key={key}")]
        Task<MapsObject> GetGeocode(string address, string key);

        [Get("/geocode/json?address={address}&key={key}")]
        Task<dynamic> GetGeocodeDynamic(string address, string key);

        [Get("/geocode/json?latlng={latlng}&key={key}")]
        Task<MapsObject> GetReverseGeocode(string latlng, string key);

    }

    /// <summary>
    /// Ver:
    /// https://paulcbetts.github.io/refit/
    /// https://developers.google.com/maps/documentation/geocoding/start#reverse
    /// </summary>
    public class GoogleMapsServices
    {
        private const string hostUrl = "https://maps.googleapis.com/maps/api";
        private readonly string key = "";

        public GoogleMapsServices(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Chave da API do google é obrigatória.", nameof(key));
            }

            this.key = key;

        }
        public async Task<Location> GetFirstLocation(string address)
        {
            var googleMapsApi = RestService.For<IGoogleMapsApi>(hostUrl);
            var mapsobj = await googleMapsApi.GetGeocode(FormatAddress(address), key);
            if (mapsobj.Results.Count == 0)
            {
                throw new Exception("Endereço inválido.");
            }
            return mapsobj.Results[0].Geometry.Location;
        }
        public async Task<string> GetAddress(float lat, float lng)
        {
            var googleMapsApi = RestService.For<IGoogleMapsApi>(hostUrl);
            var mapsobj = await googleMapsApi.GetReverseGeocode($"{lat},{lng}", key);
            if (mapsobj.Results.Count == 0)
            {
                throw new Exception("Nenhum endereço encontrado nesta geolocalização.");
            }
            return mapsobj.Results[0].FormattedAddress;
        }
        public async Task<dynamic> GetGeocodeDynamic(string address)
        {
            var googleMapsApi = RestService.For<IGoogleMapsApi>(hostUrl);
            return await (dynamic)googleMapsApi.GetGeocodeDynamic(FormatAddress(address), key);
        }

        private string FormatAddress(string address)
        {
            return address.Replace(' ', '+');
        }
    }

    public class MapsObject
    {
        public List<Result> Results { get; set; }
    }
    public class Result
    {
        [JsonProperty(PropertyName = "formatted_address")]
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public float Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public float Longitude { get; set; }

        public override string ToString()
        {
            return $"Latitude: {Latitude}; Longitude: {Longitude}";
        }
    }
}
