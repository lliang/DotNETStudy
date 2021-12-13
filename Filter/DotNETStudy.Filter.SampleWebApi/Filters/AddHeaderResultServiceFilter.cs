using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// 结果筛选器：
    /// 实现接口：
    /// IResultFilter 或 IAsyncResultFilter
    /// IAlwaysRunResultFilter 或 IAsyncAlwaysRunResultFilter
    /// 
    /// 它们的执行围绕着操作结果的执行。
    /// 
    /// 仅当操作或操作筛选器生成操作结果时，才会执行结果筛选器。
    /// 不会在以下情况下执行结果筛选器：
    /// 授权筛选器或资源筛选器使管道短路。
    /// 异常筛选器通过生成操作结果来处理异常。
    /// 
    /// Microsoft.AspNetCore.Mvc.Filters.IResultFilter.OnResultExecuting 方法可以将
    /// Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext.Cancel 设置为 true，
    /// 使操作结果和后续结果筛选器的执行短路。
    /// 设置短路时写入响应对象，以免生成空响应。
    /// 
    /// 如果在 IResultFilter.OnResultExecuting 中引发异常，则会导致：
    /// 阻止操作结果和后续筛选器的执行。
    /// 结果被视为失败而不是成功。
    /// 
    /// 当 Microsoft.AspNetCore.Mvc.Filters.IResultFilter.OnResultExecuted 方法运行时，响应可能已发送到客户端。
    /// 如果响应已发送到客户端，则无法更改。
    /// 如果操作结果执行已被另一个筛选器设置短路，则 ResultExecutedContext.Canceled 设置为 true。
    /// 如果操作结果或后续结果筛选器引发了异常，则 ResultExecutedContext.Exception 设置为非 NULL 值。
    /// 
    /// 将 Exception 设置为 NULL 可有效地处理异常，并防止在管道的后续阶段引发该异常。
    /// 处理结果筛选器中出现的异常时，没有可靠的方法来将数据写入响应。
    /// 如果在操作结果引发异常时标头已刷新到客户端，则没有任何可靠的机制可用于发送失败代码。
    /// 
    /// 对于 IAsyncResultFilter，通过调用 ResultExecutionDelegate 上的 await next 可执行所有后续结果筛选器和操作结果。
    /// 若要短路，请设置 ResultExecutingContext.Cancel 为 true ，不调用 ResultExecutionDelegate
    /// 
    /// 该框架提供一个可子类化的抽象 ResultFilterAttribute
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
            var headerName = "OnResultEcecuting";
            context.HttpContext.Response.Headers.Add(headerName, new string[] { "ResultExecutingSuccessfully" });
            _logger.LogInformation("Header added: {HeaderName}", headerName);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("AddHeaderResultServiceFilter.OnResultExecuted");
        }
    }
}
