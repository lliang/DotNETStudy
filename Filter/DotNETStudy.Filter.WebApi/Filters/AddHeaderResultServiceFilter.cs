using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.WebApi.Filters
{
    /// <summary>
    /// 
    /// ServiceFilterAttribute 与 TypeFilterAttribute
    /// 
    /// 在 ConfigureServices 中注册服务筛选器实现类型。 ServiceFilterAttribute 可从 DI 检索筛选器实例。
    /// 
    /// ServiceFilter 属性将从 DI 中检索 AddHeaderResultServiceFilter 筛选器的实例：
    /// [ServiceFilter(typeof(AddHeaderResultServiceFilter))]
    /// 
    /// ServiceFilterAttribute 可实现 IFilterFactory。
    /// IFilterFactory 公开用于创建 IFilterMetadata 实例的 CreateInstance 方法。
    /// CreateInstance 从 DI 中加载指定的类型。
    /// 
    /// TypeFilterAttribute 与 ServiceFilterAttribute 类似，但不会直接从 DI 容器解析其类型。
    /// 它使用 Microsoft.Extensions.DependencyInjection.ObjectFactory 对类型进行实例化。
    /// 
    /// 因为不会直接从 DI 容器解析 TypeFilterAttribute 类型：
    /// 使用 TypeFilterAttribute 引用的类型不需要注册在 DI 容器中。 它们具备由 DI 容器实现的依赖项。
    /// TypeFilterAttribute 可以选择为类型接受构造函数参数。
    /// 
    /// </summary>
    public class AddHeaderResultServiceFilter : IResultFilter
    {
        private ILogger _logger;
        public AddHeaderResultServiceFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AddHeaderResultServiceFilter>();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var headerName = "OnResultExecuting";
            context.HttpContext.Response.Headers.Add(headerName, new string[] { "ResultExecutingSuccessfully" });
            _logger.LogInformation("Header added: {HeaderName}", headerName);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("AddHeaderResultServiceFilter.OnResultExecuted");
        }
    }
}
