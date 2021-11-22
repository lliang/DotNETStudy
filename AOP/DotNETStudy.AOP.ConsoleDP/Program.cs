using AspectCore.Configuration;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.DependencyInjection;
using DotNETStudy.AOP.ConsoleDP;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var hostBuilder = new HostBuilder();
hostBuilder.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

hostBuilder.ConfigureServices(services =>
{
    services.AddTransient<IService, Service>();
    services.ConfigureDynamicProxy(config =>
    {
        config.Interceptors.AddTyped<EnableParameterAspectInterceptor>();
    });
});

var host = hostBuilder.Build();
var service = host.Services.GetService<IService>();

var sample = new Sample { Email = "123", Url = "123", MaxLengthValue = "123" };
sample.SetValidationHandler(new ThrowHandler());

service?.Run(sample);

