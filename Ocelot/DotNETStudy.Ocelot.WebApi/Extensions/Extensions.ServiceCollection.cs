using Ocelot.Provider.Consul;
using Ocelot.DependencyInjection;

namespace DotNETStudy.Ocelot.WebApi.Extensions
{
    public static class Extensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
            services.AddOcelot(config).AddConsul();
        }
    }
}
