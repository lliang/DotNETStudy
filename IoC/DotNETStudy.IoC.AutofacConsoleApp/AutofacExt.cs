using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace DotNETStudy.IoC.AutofacConsoleApp
{
    public static class AutofacExt
    {
        private static IContainer container;

        public static void InitAutofac()
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile("Config/AutofacConfig.json");

            var module = new ConfigurationModule(config.Build());

            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            container = builder.Build();
        }

        public static T GetFromAutofac<T>() => container.Resolve<T>();

        public static T GetFromAutofac<T>(string name) => container.ResolveNamed<T>(name);
    }
}
