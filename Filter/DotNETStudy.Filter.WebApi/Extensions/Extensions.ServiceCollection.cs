using DotNETStudy.Filter.WebApi.Filters;

namespace DotNETStudy.Filter.WebApi.Extensions
{
    /// <summary>
    /// 可以将筛选器添加到管道中的三个范围之一：
    /// 在控制器操作上使用属性。 筛选器属性不能应用于 Pages Razor 处理程序方法。
    /// 在控制器或页上使用 Razor 属性。
    /// 全局所有控制器、操作和 Razor Pages。
    /// </summary>
    public static partial class Extensions
    {
        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddScoped<AddHeaderResultServiceFilter>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(MySampleActionFilter));
            });
            return services;
        }
    }
}
