using Dapr.Client;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGrpcClient
{
    class Program
    {
        private static string LOCAL_ADDRESS = @"http://localhost:5000";
        private static string DAPR_ADDRESS = $"http://localhost:55320";

        static async Task Main(string[] args)
        {
            await CallToLocalHost().ConfigureAwait(false); // runs OK
            await CallWithClient().ConfigureAwait(false); // exception: 
        }

        private static async Task CallToLocalHost()
        {
            using var channel = GrpcChannel.ForAddress(LOCAL_ADDRESS);
            var cl = new MyGrpcService.Greeter.GreeterClient(channel);
            MyGrpcService.HelloReply response = await cl.SayHelloAsync(new MyGrpcService.HelloRequest { Name = "ThinkPad" });
            Console.WriteLine(response.Message);
        }

        private static async Task CallWithClient()
        {
            using DaprClient client = new DaprClientBuilder()
                     .UseGrpcEndpoint(DAPR_ADDRESS)
                     .Build();

            var request = new MyGrpcService.HelloRequest { Name = "ThinkPad" };
            var result = await client.InvokeMethodGrpcAsync<MyGrpcService.HelloRequest, MyGrpcService.HelloReply>("MyGrpcApi001", "SayHello", request).ConfigureAwait(false);
        }
    }
}
