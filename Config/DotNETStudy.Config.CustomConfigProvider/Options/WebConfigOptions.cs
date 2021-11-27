namespace DotNETStudy.Config.CustomConfigProvider
{
    internal class WebConfigOptions
    {
        public ConnectionStringsItem ConnStr1 { get; set; }

        public Smtp Smtp { get; set; }

        public string RedisServer { get; set; }

        public string RedisPassword { get; set; }
    }

    internal class ConnectionStringsItem
    {
        public string ConnectionString { get; set; }

        public string ProviderName { get; set; }
    }

    internal class Smtp
    {
        public string Server { get; set; }

        public int Port { get; set; }
    }
}
