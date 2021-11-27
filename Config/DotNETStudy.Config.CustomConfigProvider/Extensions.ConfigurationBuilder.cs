using Microsoft.Extensions.Configuration;

namespace DotNETStudy.Config.CustomConfigProvider
{
    internal static class Extensions
    {
        public static IConfigurationBuilder AddCustomConfig(this IConfigurationBuilder builder, string? path = default)
        {
            path ??= "web.config";

            builder.Add(new CustomConfigurationSource() { Path = path });

            return builder;
        }
    }
}
