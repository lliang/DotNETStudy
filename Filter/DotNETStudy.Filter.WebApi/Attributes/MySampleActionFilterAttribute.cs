using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.WebApi.Attributes
{
    public class MySampleActionFilterAttribute : IAsyncActionFilter
    {
        /*
         * 筛选器接口的同步和异步版本任意实现一个，而不是同时实现。
         * 运行时会先查看筛选器是否实现了异步接口：
         * 如果是，则调用该接口。 
         * 如果不是，则调用同步接口的方法。
         * 如果在一个类中同时实现异步和同步接口，则仅调用异步方法。
         * 使用抽象类（如 ActionFilterAttribute ）时，仅重写每个筛选器类型的同步方法或异步方法。
         */

        /*        public void OnActionExecuting(ActionExecutingContext context)
                {
                    throw new NotImplementedException();
                }

                public void OnActionExecuted(ActionExecutedContext context)
                {
                    throw new NotImplementedException();
                }*/

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
