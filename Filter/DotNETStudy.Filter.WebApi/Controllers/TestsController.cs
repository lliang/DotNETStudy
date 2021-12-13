using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DotNETStudy.Filter.WebApi.Filters;
using DotNETStudy.Filter.WebApi.Helpers;

namespace DotNETStudy.Filter.WebApi.Controllers
{
    /// <summary>
    /// 从 `Controller` 基类继承的每个控制器都 Controller 包含
    /// Controller.OnActionExecuting 、
    /// Controller.OnActionExecutionAsync 和
    /// Controller.OnActionExecuted 方法。
    /// 
    /// 覆盖为给定操作运行的筛选器。
    /// OnActionExecuting 在所有操作筛选器之前调用。
    /// OnActionExecuted 在所有操作筛选器之后调用。
    /// OnActionExecutionAsync 在所有操作筛选器之前调用。 next 之后的筛选器中的代码在操作方法之后运行。
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : Controller
    {
        [TypeFilter(typeof(MySampleActionFilter))]
        [HttpGet("TestActionFilter")]
        public IActionResult TestActionFilter()
        {
            return Ok();
        }

        [TypeFilter(typeof(MySampleAsyncActionFilter))]
        [ServiceFilter(typeof(AddHeaderResultServiceFilter))]
        [HttpGet("TestAsyncActionFilter")]
        public IActionResult TestAsyncActionFilter()
        {
            return Ok();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            MyDebug.Write(MethodBase.GetCurrentMethod(), HttpContext.Request.Path);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            MyDebug.Write(MethodBase.GetCurrentMethod(), HttpContext.Request.Path);
            base.OnActionExecuted(context);
        }
    }
}
