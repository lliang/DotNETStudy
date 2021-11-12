using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DotNETStudy.AOP.ConsoleAOP
{
    public class MyRequest : IRequest<bool>
    {
        public int Number { get; set; }
    }

    public class MyRequestHandler : IRequestHandler<MyRequest, bool>
    {
        public Task<bool> Handle(MyRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("主逻辑");
            Console.WriteLine((request.Number + 1) / request.Number);
            // new MyService().Calc(request.Number);
            return Task.FromResult(true);
        }
    }
}
