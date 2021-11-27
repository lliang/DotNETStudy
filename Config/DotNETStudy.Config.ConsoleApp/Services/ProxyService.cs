using System;
using Microsoft.Extensions.Options;
using DotNETStudy.Config.ConsoleApp.Options;

namespace DotNETStudy.Config.ConsoleApp.Services
{
    internal class ProxyService
    {
        private readonly IOptionsSnapshot<ProxyOptions> _proxyOptions;

        public ProxyService(IOptionsSnapshot<ProxyOptions> proxyOptions)
        {
            _proxyOptions = proxyOptions;
        }

        public void Print()
        {
            Console.WriteLine($"proxy: {_proxyOptions.Value.Address}:{_proxyOptions.Value.Port}");
        }
    }
}
