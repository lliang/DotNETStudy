using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// 验证模型状态。
    /// 如果状态无效，则返回错误。
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        /// <summary>
        /// OnActionExecuted 方法在操作方法之后运行：
        /// 可通过 Result 属性查看和处理操作结果。
        /// 如果操作执行已被另一个筛选器设置短路，则 Canceled 设置为 true。
        /// 如果操作或后续操作筛选器引发了异常，则 Exception 设置为非 NULL 值。 将 Exception 设置为 null：
        /// 有效地处理异常。
        /// 执行 ActionExecutedContext.Result，从操作方法中将它正常返回。
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result;
            if (context.Canceled == true)
            {

            }
            if (context.Exception != null)
            {
                context.Exception = null;
            }

            base.OnActionExecuted(context);
        }
    }
}
