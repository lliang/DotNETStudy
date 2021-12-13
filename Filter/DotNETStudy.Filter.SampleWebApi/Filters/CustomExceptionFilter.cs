using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// 异常筛选器：
    /// 实现 IExceptionFilter 或 IAsyncExceptionFilter。
    /// 可用于实现常见的错误处理策略。
    /// 
    /// 没有之前和之后的事件。
    /// 实现 OnException 或 OnExceptionAsync。
    /// 处理在 Razor 页或控制器创建、 模型绑定、操作筛选器或操作方法中发生的未经处理的异常。
    /// 不要 捕获资源 筛选器、结果筛选器或 MVC 结果执行中发生的异常。
    /// 
    /// 若要处理异常，请将 ExceptionHandled 属性设置为 true，或编写响应。 这将停止传播异常。
    /// 异常筛选器无法将异常转变为“成功”。 只有操作筛选器才能执行该转变。
    /// 
    /// 异常筛选器：
    /// 非常适合捕获发生在操作中的异常。
    /// 并不像错误处理中间件那么灵活。
    /// 
    /// 建议使用中间件处理异常。 基于所调用的操作方法，仅当错误处理不同时，才使用异常筛选器。
    /// 例如，应用可能具有用于 API 终结点和视图/HTML 的操作方法。 API 终结点可能返回 JSON 形式的错误信息，而基于视图的操作可能返回 HTML 形式的错误页。
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilter(IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }

            var result = new ViewResult { ViewName = "CustomError" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            result.ViewData.Add("Exception", context.Exception);
            context.Result = result;
        }
    }
}
