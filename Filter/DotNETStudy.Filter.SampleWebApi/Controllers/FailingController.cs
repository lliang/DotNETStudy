using Microsoft.AspNetCore.Mvc;
using DotNETStudy.Filter.SampleWebApi.Filters;

namespace DotNETStudy.Filter.SampleWebApi.Controllers
{
    [Route("[controller]")]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class FailingController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            throw new Exception("Testing custom exception filter.");
        }
    }
}
