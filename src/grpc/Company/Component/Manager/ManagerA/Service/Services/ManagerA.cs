using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using System;
using ManagerA.Interfaces;

namespace MyGrpcService.Services
{
    public class ManagerA : AppCallback.AppCallbackBase // Greeter.GreeterBase
    {
        private readonly ILogger<ManagerA> _logger;
        public ManagerA(ILogger<ManagerA> logger)
        {
            _logger = logger;
            _logger.LogInformation($">>> {nameof(ManagerA)}.Constructor");
        }

        private Task<ManagerAResponse> SayHello(ManagerARequest request, ServerCallContext context)
        {
            _logger.LogInformation($">>> {nameof(ManagerA)}.{nameof(SayHello)}");

            return Task.FromResult(new ManagerAResponse
            {
                Message = "Hello " + request.Name
            });
        }

        public Task<Empty> Test(Empty request, ServerCallContext context)
        {
            _logger.LogInformation($">>> {nameof(ManagerA)}.{nameof(Test)}");
            return Task.FromResult(new Empty());
        }

        // ========================================================================

        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {

            var response = new InvokeResponse();

            switch (request.Method)
            {
                case nameof(SayHello):
                    var input = request.Data.Unpack<ManagerARequest>();
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
