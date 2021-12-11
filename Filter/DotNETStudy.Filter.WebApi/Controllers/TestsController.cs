using DotNETStudy.Filter.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNETStudy.Filter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [TypeFilter(typeof(MySampleActionFilter))]
        [HttpGet("TestActionFilter")]
        public IActionResult TestActionFilter()
        {
            return Ok();
        }

        [TypeFilter(typeof(MySampleAsyncActionFilter))]
        [HttpGet("TestAsyncActionFilter")]
        public IActionResult TestAsyncActionFilter()
        {
            return Ok();
        }
    }
}
