﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace RefitInternalGenerated
{
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

namespace prog5
{
    using RefitInternalGenerated;

    [Preserve]
    public partial class AutoGeneratedIGoogleMapsApi : IGoogleMapsApi
    {
        public HttpClient Client { get; protected set; }
        readonly Dictionary<string, Func<HttpClient, object[], object>> methodImpls;

        public AutoGeneratedIGoogleMapsApi(HttpClient client, IRequestBuilder requestBuilder)
        {
            methodImpls = requestBuilder.InterfaceHttpMethods.ToDictionary(k => k, v => requestBuilder.BuildRestResultFuncForMethod(v));
            Client = client;
        }

        public virtual Task<MapsObject> GetGeocode(string address,string key)
        {
            var arguments = new object[] { address,key };
            return (Task<MapsObject>) methodImpls["GetGeocode"](Client, arguments);
        }

        public virtual Task<dynamic> GetGeocodeDynamic(string address,string key)
        {
            var arguments = new object[] { address,key };
            return (Task<dynamic>) methodImpls["GetGeocodeDynamic"](Client, arguments);
        }

        public virtual Task<MapsObject> GetReverseGeocode(string latlng,string key)
        {
            var arguments = new object[] { latlng,key };
            return (Task<MapsObject>) methodImpls["GetReverseGeocode"](Client, arguments);
        }

    }
}
