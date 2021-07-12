// https://docs.dapr.io/developing-applications/sdks/dotnet/dotnet-client/
// this works: iwr http://localhost:49924/v1.0/invoke/MyWebApi001/method/Weatherforecast

//string address = $"http://{args[0]}/";
//// string address = @"http://localhost:49924/";



using Dapr.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyWebClient
{
    class Program
    {
        private static string ADDRESS;

        static async Task Main(string[] args)
        {
            ADDRESS = args[0];

            IEnumerable<WeatherItem> clientResponse = await CallWithClient().ConfigureAwait(false);

            Console.WriteLine($"RESULTS count: {clientResponse.Count()}");
        }

        private static async Task<IEnumerable<WeatherItem>> CallWithClient()
        {

            using DaprClient client = new DaprClientBuilder()
                    .UseHttpEndpoint($"http://{ADDRESS}")
                    .Build();

            var weatherItems = await client.InvokeMethodAsync<WeatherItem[]>(HttpMethod.Get, "MyWebApi001", "Weatherforecast").ConfigureAwait(false);

            return weatherItems;
        }
    }

    public class WeatherItem
    {
        public DateTime date { get; set; }
        public int temperatureC { get; set; }
        public int temperatureF { get; set; }
        public string summary { get; set; }
    }

}