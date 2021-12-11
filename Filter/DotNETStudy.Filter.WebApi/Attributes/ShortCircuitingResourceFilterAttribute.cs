using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.WebApi.Attributes
{
    public class ShortCircuitingResourceFilterAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// 通过设置提供给筛选器方法的 ResourceExecutingContext 参数上的 Result 属性，可以使筛选器管道短路。
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.Result = new ContentResult()
            {
                Content = "Resource unavailable - header not set."
            };
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
