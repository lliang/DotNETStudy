using Microsoft.Extensions.Configuration;

namespace DotNETStudy.Config.CustomConfigProvider
{
    internal class CustomConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new CustomConfigurationProvider(this);
        }
    }
}
