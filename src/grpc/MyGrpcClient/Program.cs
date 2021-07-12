using Dapr.Client;
using Grpc.Net.Client;
using GrpcLabs.Interfaces;
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

        private static Task CallServiceDirectly()
        {
            return Task.CompletedTask;

            //using var channel = GrpcChannel.ForAddress(LOCAL_ADDRESS);
            //var client = new MyGrpcService.Greeter.GreeterClient(channel);
            //MyGrpcService.HelloReply response = await client.SayHelloAsync(new MyGrpcService.HelloRequest { Name = "ThinkPad" });
            //Console.WriteLine(response.Message);
        }

        private static async Task CallServiceViaDapr()
        {
            using DaprClient daprClient = new DaprClientBuilder()
                     .UseGrpcEndpoint($"http://localhost:56637")
                     .Build();

            var request = new HelloRequest { Name = "ThinkPad" };

            var result = await daprClient.InvokeMethodGrpcAsync<HelloRequest, HelloReply>("MyGrpcApi001", "SayHello", request).ConfigureAwait(false);

            


            //{
            //    var response = await DaprClient.InvokeMethodAsync<UserReadViewModel>(HttpMethod.Get, appIdAggregator, $"/v1/Bikes/{Model.User.UserId}");
            //    if (response != null)
            //    {
            //        Model = response;
            //        this.StateHasChanged();
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            //}

        }
    }
}
