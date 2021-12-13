using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using DotNETStudy.Filter.SampleWebApi.Filters;

namespace DotNETStudy.Filter.SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        [HttpGet("header")]
        [AddHeaderWithFactory]
        public IActionResult HeaderWithFactory()
        {
            return Content("Examine the headers using the F12 developer tools.");
        }

        /// <summary>
        /// 使用 MiddlewareFilterAttribute 运行中间件
        /// 中间件筛选器与资源筛选器在筛选器管道的相同阶段运行，即，在模型绑定之前以及管道的其余阶段之后。
        /// </summary>
        /// <returns></returns>
        [HttpGet("culture")]
        [MiddlewareFilter(typeof(LocalizationPipeline))]
        public IActionResult CultureFromRouteData()
        {
            return Content(
                $"CurrentCulture: {CultureInfo.CurrentCulture.Name},"
                + $"CurrentUICulture: {CultureInfo.CurrentUICulture.Name}");
        }
    }
}
