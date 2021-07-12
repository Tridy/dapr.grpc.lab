using Dapr.Client;
using Grpc.Net.Client;
using GrpcLabs.Interfaces;
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

            var request = new HelloRequest { Name = "ThinkPad" };

            var result = await daprClient.InvokeMethodGrpcAsync<HelloRequest, HelloReply>("MyGrpcApi001", "SayHello", request).ConfigureAwait(false);

            Console.WriteLine($"Got reply: {result.Message}");

            Console.ReadKey();
        }
    }
}
