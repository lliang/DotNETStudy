using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNETStudy.Config.CustomConfigProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            //configurationBuilder.Add(new CustomConfigurationSource() { Path = "web.config" });
            configurationBuilder.AddCustomConfig("web.config");

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            ServiceCollection services = new ServiceCollection();

            services.AddOptions().Configure<WebConfigOptions>(w => configurationRoot.Bind(w));

            services.AddTransient<WebConfigService>();

            using (var sp = services.BuildServiceProvider())
            {
                var webConfigService = sp.GetRequiredService<WebConfigService>();
                webConfigService.Print();
            }
        }
    }
}