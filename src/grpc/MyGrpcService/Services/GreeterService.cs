using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using GrpcLabs.Interfaces;
using System;

namespace MyGrpcService.Services
{
    public class GreeterService : AppCallback.AppCallbackBase // Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            _logger.LogInformation($">>> {nameof(GreeterService)}.Constructor");
        }

        private Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($">>> {nameof(GreeterService)}.{nameof(SayHello)}");

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public Task<Empty> Test(Empty request, ServerCallContext context)
        {
            _logger.LogInformation($">>> {nameof(GreeterService)}.{nameof(Test)}");
            return Task.FromResult(new Empty());
        }

        // ========================================================================

        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {

            var response = new InvokeResponse();

            switch (request.Method)
            {
                case nameof(SayHello):
                    var input = request.Data.Unpack<HelloRequest>();
                    var output = await SayHello(input, context);
                    response.Data = Any.Pack(output);
                    break;
                default:
                    throw new NotSupportedException($"The method {request.Method} is not supported");

            }
            return response;
        }

        public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new ListTopicSubscriptionsResponse());
        }

        public override Task<TopicEventResponse> OnTopicEvent(TopicEventRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TopicEventResponse());
        }

        public override Task<ListInputBindingsResponse> ListInputBindings(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new ListInputBindingsResponse());
        }

        public override Task<BindingEventResponse> OnBindingEvent(BindingEventRequest request, ServerCallContext context)
        {
            return Task.FromResult(new BindingEventResponse());
        }
    }
}
