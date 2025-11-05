using System;

namespace NetworkSetter
{
    public class NetworkConfig
    {
        public string Name { get; set; } = string.Empty;
        public string AdapterName { get; set; } = string.Empty;
        public string IpVersion { get; set; } = "IPv4"; // IPv4 or IPv6
        public bool UseDhcp { get; set; } = false;
        public string IpAddress { get; set; } = string.Empty;
        public string SubnetMask { get; set; } = string.Empty;
        public string Gateway { get; set; } = string.Empty;
        public string PreferredDns { get; set; } = string.Empty;
        public string AlternateDns { get; set; } = string.Empty;
    }
}
