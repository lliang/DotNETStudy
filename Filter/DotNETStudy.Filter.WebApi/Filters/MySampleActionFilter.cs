using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// 筛选器通过不同的接口定义支持同步和异步实现。
/// 同步筛选器在其管道阶段之前和之后运行代码。
/// 异步筛选器定义 On-Stage-ExecutionAsync 方法。
/// </summary>
namespace DotNETStudy.Filter.WebApi.Filters
{
    /// <summary>
    /// 操作筛选器
    /// </summary>
    public class MySampleActionFilter : IActionFilter
    {
        private readonly ILogger<MySampleActionFilter> _logger;

        public MySampleActionFilter(ILogger<MySampleActionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 在调用操作方法之前调用
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod()}, {context.HttpContext.Request.Path}");
        }

        /// <summary>
        /// 在操作方法返回之后调用
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"{MethodBase.GetCurrentMethod()}, {context.HttpContext.Request.Path}");
        }
    }
}
