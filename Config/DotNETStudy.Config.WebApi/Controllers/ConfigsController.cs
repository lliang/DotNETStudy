using Microsoft.AspNetCore.Mvc;
using DotNETStudy.Config.WebApi.Options;

namespace DotNETStudy.Config.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ContentResult Get()
        {
            /*
默认的 JsonConfigurationProvider 会按以下顺序加载配置：
1. appsettings.json；
2. appsettings.Environment.json ：例如，appsettings.Production.json 和 appsettings.Development.json 。 文件的环境版本是根据 IHostingEnvironment.EnvironmentName 加载的。
在开发环境中，appsettings.Development.json 配置会覆盖在 appsettings.json 中找到的值。
在生产环境中，appsettings.Production.json 配置会覆盖在 appsettings.json 中找到的值。
             */
            var myKeyValue = _configuration["MyKey"];
            var title = _configuration["Position:Title"];
            var name = _configuration["Position:Name"];
            var defaultLogLevel = _configuration["Logging:LogLevel:Default"];

            // GetValue 方法支持默认值
            var defaultValue = _configuration.GetValue("InvalidKey", "default value");

            return Content($"MyKey value: {myKeyValue} \n" +
                $"Title: {title} \n" +
                $"Name: {name} \n" +
                $"Default Log Level: {defaultLogLevel} \n" +
                $"Default Value: {defaultValue}");
        }

        [HttpGet("Position")]
        public ContentResult GetPosition()
        {
            /*            // 调用 ConfigurationBinder.Bind 将 PositionOptions 类绑定到 Position 部分。
                        var positionOptions = new PositionOptions();
                        _configuration.GetSection(PositionOptions.Position).Bind(positionOptions);*/

            var positionOptions = _configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();

            return Content($"Title: {positionOptions.Title} \n" +
                $"Name: {positionOptions.Name}");
        }
    }
}
