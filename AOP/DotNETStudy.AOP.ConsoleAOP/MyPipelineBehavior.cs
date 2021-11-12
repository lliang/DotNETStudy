using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DotNETStudy.AOP.ConsoleAOP
{
    /* https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection/blob/master/test/MediatR.Extensions.Microsoft.DependencyInjection.Tests/PipelineTests.cs
        public class ConstrainedBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : Ping
            where TResponse : Pong
        {
            private readonly Logger _output;

            public ConstrainedBehavior(Logger output)
            {
                _output = output;
            }

            public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
            {
                _output.Messages.Add("Constrained before");
                var response = await next();
                _output.Messages.Add("Constrained after");

                return response;
            }
        }
     */
    public class MyPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Console.WriteLine("before");
            var response = await next();
            Console.WriteLine("after");
            return response;
        }
    }
}
