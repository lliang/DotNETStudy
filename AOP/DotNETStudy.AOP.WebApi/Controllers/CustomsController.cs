using Microsoft.AspNetCore.Mvc;
using DotNETStudy.AOP.WebApi.Services;

namespace DotNETStudy.AOP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomsController : ControllerBase
    {
        private readonly ICustomService _service;
        public CustomsController(ICustomService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _service.Call();
            return Ok();
        }
    }
}
