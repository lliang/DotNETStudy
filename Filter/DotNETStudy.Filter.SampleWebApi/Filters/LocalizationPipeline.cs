using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;

namespace DotNETStudy.Filter.SampleWebApi.Filters
{
    /// <summary>
    /// 在筛选器管道中使用中间件
    /// 
    /// 资源筛选器的工作方式与中间件类似，即涵盖管道中的所有后续执行。
    /// 但筛选器又不同于中间件，它们是运行时的一部分，这意味着它们有权访问上下文和构造。
    /// </summary>
    public class LocalizationPipeline
    {
        /*
         * 若要将中间件用作筛选器，可创建一个具有 Configure 方法的类型，
         * 该方法可指定要注入到筛选器管道的中间件。
         */
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(
                    culture: "en-US",
                    uiCulture: "en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            options.RequestCultureProviders = new[]
            {
                new RouteDataRequestCultureProvider()
                {
                    Options = options
                }
            };

            applicationBuilder.UseRequestLocalization(options);
        }
    }
}
