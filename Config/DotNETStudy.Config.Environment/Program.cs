using Microsoft.Extensions.Configuration;

namespace DotNETStudy.Config.Environment
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // 无参数方法会将操作系统中的所有环境变量加载
            // configurationBuilder.AddEnvironmentVariables();
            // VS “调试”菜单项->“{namespace} 调试属性”中添加环境变量配置：Test_name=liu liang,Test_age=35
            configurationBuilder.AddEnvironmentVariables(prefix: "Test_");

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            foreach (var config in configurationRoot.AsEnumerable())
            {
                System.Console.WriteLine($"key: {config.Key}, value: {config.Value}");
            }
        }
    }
}
