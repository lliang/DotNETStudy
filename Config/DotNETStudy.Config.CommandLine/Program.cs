using Microsoft.Extensions.Configuration;

namespace DotNETStudy.Config.CommandLine
{
    class Program
    {
        // dotnet run server=127.0.0.1
        // dotnet run --server=127.0.0.1
        // dotnet run --server 127.0.0.1 --port 80 --name liuliang --age 35
        // dotnet run /server=127.0.0.1
        // dotnet run /server 127.0.0.1
        // 复杂结构的配置需要进行“扁平化处理”：a:b:c={value}、a:b:c:0={array item value}
        // dotnet run proxy:address=127.0.0.1 proxy:port=1080 proxy:ids:0=10 proxy:ids:1=20
        static void Main(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddCommandLine(args);

            IConfigurationRoot configRoot = configBuilder.Build();
            foreach (var config in configRoot.AsEnumerable())
            {
                System.Console.WriteLine($"key: {config.Key}, value: {config.Value}");
            }
        }
    }
}
