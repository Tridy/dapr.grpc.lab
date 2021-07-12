using Dapr.Client;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace MyGrpcClient
{
    class Program
    {
        private static string LOCAL_ADDRESS = @"http://localhost:5000";
        private static string DAPR_ADDRESS = $"http://localhost:56637";

        static async Task Main(string[] args)
        {
            await CallServiceDirectly().ConfigureAwait(false); // runs OK
            await CallServiceViaDapr().ConfigureAwait(false); // exception: 
        }

        private static async Task CallServiceDirectly()
        {
            using var channel = GrpcChannel.ForAddress(LOCAL_ADDRESS);
            var client = new MyGrpcService.Greeter.GreeterClient(channel);
            MyGrpcService.HelloReply response = await client.SayHelloAsync(new MyGrpcService.HelloRequest { Name = "ThinkPad" });
            Console.WriteLine(response.Message);
        }

        private static async Task CallServiceViaDapr()
        {
            using DaprClient daprClient = new DaprClientBuilder()
                     .UseGrpcEndpoint(DAPR_ADDRESS)
                     .Build();

            var request = new MyGrpcService.HelloRequest { Name = "ThinkPad" };

            var result = await daprClient.InvokeMethodGrpcAsync<MyGrpcService.HelloRequest, MyGrpcService.HelloReply>("MyGrpcApi001", "SayHello", request).ConfigureAwait(false);
        }
    }
}
