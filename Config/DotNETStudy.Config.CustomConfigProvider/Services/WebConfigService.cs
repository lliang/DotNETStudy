using System;
using Microsoft.Extensions.Options;

namespace DotNETStudy.Config.CustomConfigProvider
{
    internal class WebConfigService
    {
        private IOptionsSnapshot<WebConfigOptions> _options;

        public WebConfigService(IOptionsSnapshot<WebConfigOptions> options)
        {
            _options = options;
        }

        public void Print()
        {
            Console.WriteLine(_options.Value.ConnStr1.ConnectionString);
            Console.WriteLine(_options.Value.ConnStr1.ProviderName);

            Console.WriteLine($"{_options.Value.Smtp.Server}:{_options.Value.Smtp.Port}");
        }
    }
}
