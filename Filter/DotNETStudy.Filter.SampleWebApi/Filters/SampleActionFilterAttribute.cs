using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// 实现 IFilterFactory 的筛选器可用于以下筛选器：
    /// 不需要传递参数。
    /// 具备需要由 DI 填充的构造函数依赖项。
    /// 
    /// TypeFilterAttribute 可实现 IFilterFactory。
    /// IFilterFactory 公开用于创建 IFilterMetadata 实例的 CreateInstance 方法。
    /// CreateInstance 从服务容器 (DI) 中加载指定的类型。
    /// 
    /// </summary>
    public class SampleActionFilterAttribute : TypeFilterAttribute
    {
        /*
以下代码显示应用 [SampleActionFilter] 的三种方法：

[SampleActionFilter]
public IActionResult FilterTest()
{
    return Content("From FilterTest");
}

[TypeFilter(typeof(SampleActionFilterAttribute))]
public IActionResult TypeFilterTest()
{
    return Content("From TypeFilterTest");
}

// ServiceFilter must be registered in ConfigureServices or
// System.InvalidOperationException: No service for type '<filter>'
// has been registered. Is thrown.
[ServiceFilter(typeof(SampleActionFilterAttribute))]
public IActionResult ServiceFilterTest()
{
    return Content("From ServiceFilterTest");
}
        */
        public SampleActionFilterAttribute() : base(typeof(SampleActionFilterImpl))
        {
        }

        private class SampleActionFilterImpl : IActionFilter
        {
            private readonly ILogger _logger;
            public SampleActionFilterImpl(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<SampleActionFilterAttribute>();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _logger.LogInformation("SampleActionFilterAttribute.OnActionExecuting");
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _logger.LogInformation("SampleActionFilterAttribute.OnActionExecuted");
            }
        }
    }
}
