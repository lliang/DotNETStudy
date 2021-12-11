using System;
using Microsoft.Extensions.Options;
using DotNETStudy.Config.ConsoleApp.Options;

namespace DotNETStudy.Config.ConsoleApp.Services
{
    internal class WcfService
    {
        private readonly IOptionsSnapshot<WcfServicesOptions> _options;

        public WcfService(IOptionsSnapshot<WcfServicesOptions> options)
        {
            _options = options;
        }

        public void Print()
        {
            foreach (var proxyConfig in _options.Value.ProxyConfigs)
            {
                Console.WriteLine(proxyConfig.Name);
            }
        }
    }
}
