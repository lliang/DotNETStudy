using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /*
     * 操作筛选器：
     * 实现 IActionFilter 或 IAsyncActionFilter 接口。
     * 它们的执行围绕着操作方法的执行。
     * 
     * ActionExecutingContext 提供以下属性：
     * 
     * ActionArguments - 用于读取操作方法的输入。
     * Controller - 用于处理控制器实例。
     * Result - 设置 Result 会使操作方法和后续操作筛选器的执行短路。
     * Canceled - 如果操作执行已被另一个筛选器设置短路，则为 true。
     * Exception - 如果操作或之前运行的操作筛选器引发了异常，则为非 NULL 值。 将此属性设置为 null：
     * 有效地处理异常。
     * 执行 Result，从操作方法中将它返回。
     * 
     * 在操作方法中引发异常：
     * 防止运行后续筛选器。
     * 与设置 Result 不同，结果被视为失败而不是成功。
     * 
     * 对于 IAsyncActionFilter，一个向 ActionExecutionDelegate 的调用可以达到以下目的：

     * 执行所有后续操作筛选器和操作方法。
     * 返回 ActionExecutedContext。
     * 
     * 若要设置短路，可将 Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext.Result 分配到某个结果实例，并且不调用 next (ActionExecutionDelegate)。
     * 该框架提供一个可子类化的抽象 ActionFilterAttribute。
     * 
     * OnActionExecuting 操作筛选器可用于：
     * 验证模型状态。
     * 如果状态无效，则返回错误。
     */

    /// <summary>
    /// 操作筛选器
    /// </summary>
    public class MySampleActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine($"Executing: {MethodBase.GetCurrentMethod()}, {context.HttpContext.Request.Path}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine($"Executed: {MethodBase.GetCurrentMethod()}, {context.HttpContext.Request.Path}");
        }
    }
}
