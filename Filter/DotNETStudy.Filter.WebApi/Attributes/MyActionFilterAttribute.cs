using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using DotNETStudy.Filter.WebApi.Options;

namespace DotNETStudy.Filter.WebApi.Attributes
{
    /// <summary>
    /// 可按类型或实例添加筛选器。
    /// 如果添加实例，该实例将用于每个请求。
    /// 如果添加类型，则将激活该类型。
    /// 
    /// 激活类型的筛选器意味着：
    /// 将为每个请求创建一个实例。
    /// 依赖关系注入(DI) 将填充所有构造函数依赖项。
    /// 
    /// 如果将筛选器作为属性实现并直接添加到控制器类或操作方法中，则该筛选器不能由依赖关系注入 (DI) 提供构造函数依赖项。
    /// 无法由 DI 提供构造函数依赖项，因为：
    /// 属性在应用时必须提供自己的构造函数参数。
    /// 这是属性工作原理上的限制。
    /// 
    /// 以下筛选器支持从 DI 提供的构造函数依赖项：
    /// ServiceFilterAttribute
    /// TypeFilterAttribute
    /// 在属性上实现 IFilterFactory。
    /// 
    /// 可以从 DI 获取记录器。 但是，避免创建和使用筛选器仅用于日志记录。 内置框架日志记录通常提供日志记录所需的内容。
    /// 添加到筛选器的日志记录：
    /// 应重点关注业务域问题或特定于筛选器的行为。
    /// 不应记录操作或其他框架事件。 内置筛选器记录操作和框架事件。
    /// </summary>
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        private readonly IOptionsSnapshot<PositionOptions> _settings;

        public MyActionFilterAttribute(IOptionsSnapshot<PositionOptions> options)
        {
            _settings = options;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_settings.Value.Title, new string[] { _settings.Value.Name });
            base.OnResultExecuting(context);
        }
    }
}
