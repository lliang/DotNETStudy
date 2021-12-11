using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.WebApi.Filters
{
    public class MySampleAsyncActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<MySampleAsyncActionFilter> _logger;

        public MySampleAsyncActionFilter(ILogger<MySampleAsyncActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Do something before the action executes. {MethodBase.GetCurrentMethod()}, {context.HttpContext.Request.Path}");

            var resultContext = await next();

            resultContext.Result = new JsonResult(new { data = DateTime.UtcNow });

            _logger.LogInformation($"Do something after the action executes. {MethodBase.GetCurrentMethod()}, {context.HttpContext.Request.Path}");
        }
    }
}
