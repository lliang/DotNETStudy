using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// IAlwaysRunResultFilter 和 IAsyncAlwaysRunResultFilter 接口声明了一个针对所有操作结果运行的
    /// IResultFilter 实现。
    /// 这包括由以下对象生成的操作结果：
    /// 设置短路的授权筛选器和资源筛选器。
    /// 异常筛选器。
    /// </summary>
    public class UnprocessableResultFilter : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is StatusCodeResult statusCodeResult && statusCodeResult.StatusCode == (int)HttpStatusCode.UnsupportedMediaType)
            {
                context.Result = new ObjectResult("Can't process this!")
                {
                    StatusCode = (int)HttpStatusCode.UnsupportedMediaType
                };
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
