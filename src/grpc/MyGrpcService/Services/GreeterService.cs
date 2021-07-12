using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyGrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            _logger.LogInformation($">>> {nameof(GreeterService)}.Constructor");
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($">>> {nameof(GreeterService)}.{nameof(SayHello)}");

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<Empty> Test(Empty request, ServerCallContext context)
        {
            _logger.LogInformation($">>> {nameof(GreeterService)}.{nameof(Test)}");
            return Task.FromResult(new Empty());
        }
    }
}
