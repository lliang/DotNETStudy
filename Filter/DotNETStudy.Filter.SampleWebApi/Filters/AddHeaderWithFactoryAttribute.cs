using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// IFilterFactory 可实现 IFilterMetadata。
    /// 因此，IFilterFactory 实例可在筛选器管道中的任意位置用作 IFilterMetadata 实例。
    /// 
    /// 当运行时准备调用筛选器时，它会尝试将其转换为 IFilterFactory。
    /// 如果转换成功，则调用 CreateInstance 方法来创建将调用的 IFilterMetadata 实例。
    /// 
    /// 这提供了一种很灵活的设计，因为无需在应用启动时显式设置精确的筛选器管道。
    /// 
    /// 可以使用自定义属性实现来实现 IFilterFactory 作为另一种创建筛选器的方法
    /// </summary>
    public class AddHeaderWithFactoryAttribute : Attribute, IFilterFactory
    {
        /*
         * IFilterFactory.IsReusable:
         * 工厂的提示，指出工厂创建的筛选器实例可以在创建工厂的请求范围之外重复使用
         * 不应 与依赖于生存期不是单一性的服务的筛选器一起使用。
         * 
         * ASP.NET Core 运行时不保证：
         * 将创建筛选器的单一实例。
         * 稍后不会从 DI 容器重新请求筛选器。
         * 
         * 只有在 IFilterFactory.IsReusable 筛选器的源明确、
         * 筛选器是无状态的且筛选器可以跨多个 HTTP 请求安全使用时，才配置为 true 返回 。 
         * 
         */
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new InternalAddHeaderFilter();
        }

        private class InternalAddHeaderFilter : IResultFilter
        {
            public void OnResultExecuting(ResultExecutingContext context)
            {
                context.HttpContext.Response.Headers.Add("Internal", new string[] { "My header" });
            }

            public void OnResultExecuted(ResultExecutedContext context)
            {
            }
        }
    }
}
