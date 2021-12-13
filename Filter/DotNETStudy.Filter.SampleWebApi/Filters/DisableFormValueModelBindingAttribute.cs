using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// 资源筛选器
    /// 
    /// 实现 IResourceFilter 或 IAsyncResourceFilter 接口。
    /// 执行会覆盖筛选器管道的绝大部分。
    /// 只有 授权筛选器 才会在资源筛选器之前运行。
    /// 
    /// 如果要使大部分管道短路，资源筛选器会很有用。 例如，如果缓存命中，则缓存筛选器可以绕开管道的其余阶段。
    /// 
    /// 可以防止模型绑定访问表单数据。
    /// 用于上传大型文件，以防止表单数据被读入内存。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var formValueProviderFactory = context.ValueProviderFactories.OfType<FormValueProviderFactory>().FirstOrDefault();
            if (formValueProviderFactory != null)
            {
                context.ValueProviderFactories.Remove(formValueProviderFactory);
            }

            var jqueryFormValueProviderFactory = context.ValueProviderFactories.OfType<JQueryFormValueProviderFactory>().FirstOrDefault();
            if (jqueryFormValueProviderFactory != null)
            {
                context.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
