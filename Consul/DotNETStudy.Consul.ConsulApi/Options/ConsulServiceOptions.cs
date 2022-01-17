namespace DotNETStudy.Consul.ConsulApi.Options
{
    // ConsulService 选项类型
    public class ConsulServiceOptions
    {
        /// <summary>
        /// ConsulService 配置节
        /// </summary>
        public const string ConsulService = "ConsulService";

        /// <summary>
        /// 服务注册地址（Consul 的地址）
        /// </summary>
        public string ConsulAddress { get; set; }

        /// <summary>
        /// 服务 Id
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthCheck { get; set; }
    }
}
