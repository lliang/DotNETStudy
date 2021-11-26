using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotNETStudy.Config.ConsoleApp.Options;
using DotNETStudy.Config.ConsoleApp.Services;

namespace DotNETStudy.Config.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configRoot = configBuilder.Build();

            ServiceCollection services = new ServiceCollection();
            services.AddOptions().Configure<ProxyOptions>(p => configRoot.GetSection(ProxyOptions.Proxy).Bind(p));

            services.AddTransient<ProxyService>();

            using (var sp = services.BuildServiceProvider())
            {
                while (true)
                {
                    /*
                    var proxyService1 = sp.GetRequiredService<ProxyService>();
                    proxyService1.Print();

                    var proxyService2 = sp.GetRequiredService<ProxyService>();
                    proxyService2.Print();

                    System.Console.WriteLine("点击任意键继续");
                    System.Console.ReadKey();
                    */

                    using (var scop = sp.CreateScope())
                    {
                        var proxyService1 = scop.ServiceProvider.GetRequiredService<ProxyService>();
                        proxyService1.Print();

                        // IOptionsSnapshot: 如果两次读取配置中间，配置有修改，读取的值还是修改前的，直到下一个 Scope，读取修改后的值
                        System.Console.WriteLine("读取完成，可以修改配置，按任意键继续读取，验证是否读取到的是修改前的值");
                        System.Console.ReadKey();

                        var proxyService2 = scop.ServiceProvider.GetRequiredService<ProxyService>();
                        proxyService2.Print();
                    }

                    System.Console.WriteLine("点击任意键继续");
                    System.Console.ReadKey();
                }
            }
        }

        static void Main2(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configRoot = configBuilder.Build();

            ServiceCollection services = new ServiceCollection();
            services.AddOptions().Configure<ProxyOptions>(p => configRoot.GetSection(ProxyOptions.Proxy).Bind(p));

            services.AddTransient<ProxyService>();

            using var sp = services.BuildServiceProvider();
            var proxyService = sp.GetRequiredService<ProxyService>();
            proxyService.Print();
        }

        static void Main1(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configRoot = configBuilder.Build();

            /*
            string name = configRoot["name"];

            string proxyAddress = configRoot.GetSection("proxy:address").Value;

            System.Console.WriteLine(name);

            System.Console.WriteLine(proxyAddress);
            */

            Config config = configRoot.Get<Config>();

            System.Console.WriteLine($"name: {config.Name}, age: {config.Age}");
            System.Console.WriteLine($"proxy: {config.Proxy.Address}:{config.Proxy.Port}");

            /*
            Proxy proxy = configRoot.GetSection("proxy").Get<Proxy>();

            System.Console.WriteLine($"Proxy: {proxy.Address}:{proxy.Port}");
            */
        }
    }

    class Config
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Proxy Proxy { get; set; }
    }

    class Proxy
    {
        public string Address { get; set; }

        public int Port { get; set; }
    }
}