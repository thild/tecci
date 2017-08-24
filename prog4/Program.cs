using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prog4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (!File.Exists("in.txt"))
            {
                return;
            }
            using (var sr = new StreamReader("in.txt"))
            {
                using (var sw = new StreamWriter("out.txt"))
                {
                    var input = await sr.ReadToEndAsync();
                    var regex = new Regex("[aA]");
                    var output = regex.Replace(input, "@");
                    await sw.WriteAsync(output);
                }
            }
        }
    }
}
