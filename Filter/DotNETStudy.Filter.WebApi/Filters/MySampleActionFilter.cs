using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

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
