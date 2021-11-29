using Microsoft.AspNetCore.Mvc;

namespace DotNETStudy.Config.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IHostEnvironment _environment;

        public EnvironmentsController(IHostEnvironment hostEnvironment)
        {
            _environment = hostEnvironment;
        }

        [HttpGet]
        public ContentResult Get()
        {
            var name = _environment.ApplicationName;
            var path = _environment.ContentRootPath;
            var provider = _environment.ContentRootFileProvider;

            return Content($"Name: {name} \n" +
                $"Path: {path} \n" +
                $"Provider: {provider}");
        }
    }
}
