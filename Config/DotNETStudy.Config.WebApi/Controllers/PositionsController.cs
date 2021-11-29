using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DotNETStudy.Config.WebApi.Options;

namespace DotNETStudy.Config.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        // private readonly PositionOptions _options;
        private readonly IOptionsSnapshot<PositionOptions> _options;

        public PositionsController(IOptionsSnapshot<PositionOptions> options)
        {
            _options = options;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return Ok($"Title: {_options.Title} \n" +
            //    $"Name: {_options.Name}");
            return Ok($"Title: {_options.Value.Title} \n" + $"Name: {_options.Value.Name}");
        }
    }
}
