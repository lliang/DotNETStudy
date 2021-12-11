using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNETStudy.Filter.WebApi.Attributes
{
    /// <summary>
    /// 响应添加标头
    /// 继承自内置筛选器属性：<see cref="ResultFilterAttribute"/>
    /// 知识点：
    /// 1. ASP.NET Core 包含许多可子类化和自定义的基于属性的内置筛选器；
    /// 2. 通过使用属性，筛选器可接收参数;
    /// 3. 多种筛选器接口具有相应属性，这些属性可用作自定义实现的基类:
    ///    筛选器属性：
    ///    ActionFilterAttribute
    ///    ExceptionFilterAttribute
    ///    ResultFilterAttribute
    ///    FormatFilterAttribute
    ///    ServiceFilterAttribute
    ///    TypeFilterAttribute
    /// </summary>
    public class AddHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public AddHeaderAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, new string[] { _value });
            base.OnResultExecuting(context);
        }
    }
}
