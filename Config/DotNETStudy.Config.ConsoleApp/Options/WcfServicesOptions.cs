using System.Collections.Generic;

namespace DotNETStudy.Config.ConsoleApp.Options
{
    /// <summary>
    /// WCF 服务选项类型
    /// </summary>
    public class WcfServicesOptions
    {
        /// <summary>
        /// WcfServices 配置节
        /// </summary>
        public const string WcfServices = "WcfServices";

        public ICollection<ProxyConfig> ProxyConfigs { get; set; } = new List<ProxyConfig>();
    }
}
