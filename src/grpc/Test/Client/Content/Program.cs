using Dapr.Client;
using ManagerA.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyGrpcClient
{
    class Program
    {
        private static string DAPR_ADDRESS = $"http://localhost:56637";

        static async Task Main(string[] args)
        {
            await CallServiceViaDapr().ConfigureAwait(false); // exception: 
        }

        private static async Task CallServiceViaDapr()
        {
            using DaprClient daprClient = new DaprClientBuilder()
                     .UseGrpcEndpoint($"http://localhost:56637")
                     .Build();

            var request = new ManagerARequest { Name = "ThinkPad" };

            var result = await daprClient.InvokeMethodGrpcAsync<ManagerARequest, ManagerAResponse>("ManagerA", "SayHello", request).ConfigureAwait(false);

            Console.WriteLine($"Got reply: {result.Message}");
            Console.WriteLine();
            Console.WriteLine("Any key to exit");

            Console.ReadKey();
        }
    }
}
