namespace DotNETStudy.Config.ConsoleApp.Options
{
    internal class ProxyOptions
    {
        public const string Proxy = "proxy";

        public string Address { get; set; }

        public int Port { get; set; }
    }
}
