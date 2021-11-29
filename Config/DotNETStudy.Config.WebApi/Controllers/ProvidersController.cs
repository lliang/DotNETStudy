using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace DotNETStudy.Config.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private IConfigurationRoot _config;

        public ProvidersController(IConfiguration configuration)
        {
            // IConfigurationRoot : IConfiguration
            _config = (IConfigurationRoot)configuration;
        }

        [HttpGet]
        public ContentResult Get()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var provider in _config.Providers)
            {
                sb.AppendLine(provider?.ToString());
            }

            return Content(sb.ToString());
        }
    }
}
