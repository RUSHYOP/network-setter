using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;

namespace NetworkSetter
{
    public class NetworkManager
    {
        public static List<string> GetNetworkAdapters()
        {
            var adapters = new List<string>();
            
            try
            {
                var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.NetworkInterfaceType != NetworkInterfaceType.Loopback && 
                                 ni.OperationalStatus == OperationalStatus.Up)
                    .Select(ni => ni.Name);
                
                adapters.AddRange(interfaces);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get network adapters: {ex.Message}");
            }
            
            return adapters;
        }

        public static NetworkConfig GetCurrentConfig(string adapterName, string ipVersion)
        {
            var config = new NetworkConfig
            {
                AdapterName = adapterName,
                IpVersion = ipVersion
            };

            try
            {
                var adapter = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(ni => ni.Name == adapterName);

                if (adapter == null)
                    return config;

                var ipProps = adapter.GetIPProperties();

                if (ipVersion == "IPv4")
                {
                    var ipv4Props = ipProps.GetIPv4Properties();
                    var unicastAddresses = ipProps.UnicastAddresses
                        .Where(ua => ua.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                    foreach (var addr in unicastAddresses)
                    {
                        config.IpAddress = addr.Address.ToString();
                        config.SubnetMask = addr.IPv4Mask.ToString();
                        break;
                    }

                    var gateway = ipProps.GatewayAddresses
                        .FirstOrDefault(ga => ga.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    if (gateway != null)
                        config.Gateway = gateway.Address.ToString();

                    var dnsServers = ipProps.DnsAddresses
                        .Where(dns => dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        .ToList();
                    
                    if (dnsServers.Count > 0)
                        config.PreferredDns = dnsServers[0].ToString();
                    if (dnsServers.Count > 1)
                        config.AlternateDns = dnsServers[1].ToString();
                }
                else // IPv6
                {
                    var unicastAddresses = ipProps.UnicastAddresses
                        .Where(ua => ua.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 &&
                                    !ua.Address.IsIPv6LinkLocal);

                    foreach (var addr in unicastAddresses)
                    {
                        config.IpAddress = addr.Address.ToString();
                        config.SubnetMask = addr.PrefixLength.ToString();
                        break;
                    }

                    var gateway = ipProps.GatewayAddresses
                        .FirstOrDefault(ga => ga.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);
                    if (gateway != null)
                        config.Gateway = gateway.Address.ToString();

                    var dnsServers = ipProps.DnsAddresses
                        .Where(dns => dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        .ToList();
                    
                    if (dnsServers.Count > 0)
                        config.PreferredDns = dnsServers[0].ToString();
                    if (dnsServers.Count > 1)
                        config.AlternateDns = dnsServers[1].ToString();
                }

                // Check if DHCP is enabled using WMI
                config.UseDhcp = IsDhcpEnabled(adapterName, ipVersion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get current configuration: {ex.Message}");
            }

            return config;
        }

        private static bool IsDhcpEnabled(string adapterName, string ipVersion)
        {
            try
            {
                var query = new SelectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True");
                using (var searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        var description = mo["Description"]?.ToString() ?? "";
                        if (description == adapterName || mo["SettingID"]?.ToString() == adapterName)
                        {
                            if (ipVersion == "IPv4")
                                return (bool)mo["DHCPEnabled"];
                            else
                                return false; // IPv6 DHCP check is more complex
                        }
                    }
                }
            }
            catch { }
            
            return false;
        }

        public static void ApplyConfig(NetworkConfig config)
        {
            try
            {
                if (config.UseDhcp)
                {
                    SetDhcp(config.AdapterName, config.IpVersion);
                }
                else
                {
                    SetStaticIp(config);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to apply configuration: {ex.Message}");
            }
        }

        private static void SetDhcp(string adapterName, string ipVersion)
        {
            var protocol = ipVersion == "IPv4" ? "ip" : "ipv6";
            ExecuteNetsh($"interface {protocol} set address name=\"{adapterName}\" source=dhcp");
            ExecuteNetsh($"interface {protocol} set dns name=\"{adapterName}\" source=dhcp");
        }

        private static void SetStaticIp(NetworkConfig config)
        {
            if (config.IpVersion == "IPv4")
            {
                // Set IP address
                var ipCommand = $"interface ip set address name=\"{config.AdapterName}\" static {config.IpAddress} {config.SubnetMask}";
                if (!string.IsNullOrWhiteSpace(config.Gateway))
                    ipCommand += $" {config.Gateway} 1";
                
                ExecuteNetsh(ipCommand);

                // Set DNS
                if (!string.IsNullOrWhiteSpace(config.PreferredDns))
                {
                    ExecuteNetsh($"interface ip set dns name=\"{config.AdapterName}\" static {config.PreferredDns} primary");
                    
                    if (!string.IsNullOrWhiteSpace(config.AlternateDns))
                        ExecuteNetsh($"interface ip add dns name=\"{config.AdapterName}\" {config.AlternateDns} index=2");
                }
            }
            else // IPv6
            {
                // Set IPv6 address
                var ipCommand = $"interface ipv6 set address interface=\"{config.AdapterName}\" address={config.IpAddress}";
                ExecuteNetsh(ipCommand);

                // Set Gateway
                if (!string.IsNullOrWhiteSpace(config.Gateway))
                    ExecuteNetsh($"interface ipv6 add route ::/0 interface=\"{config.AdapterName}\" nexthop={config.Gateway}");

                // Set DNS
                if (!string.IsNullOrWhiteSpace(config.PreferredDns))
                {
                    ExecuteNetsh($"interface ipv6 set dns name=\"{config.AdapterName}\" static {config.PreferredDns} primary");
                    
                    if (!string.IsNullOrWhiteSpace(config.AlternateDns))
                        ExecuteNetsh($"interface ipv6 add dns name=\"{config.AdapterName}\" {config.AlternateDns} index=2");
                }
            }
        }

        private static void ExecuteNetsh(string arguments)
        {
            var netshPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "netsh.exe");
            
            var startInfo = new ProcessStartInfo
            {
                FileName = netshPath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(startInfo))
            {
                if (process == null)
                    throw new Exception("Failed to start netsh process");

                process.WaitForExit();
                
                if (process.ExitCode != 0)
                {
                    var error = process.StandardError.ReadToEnd();
                    var output = process.StandardOutput.ReadToEnd();
                    throw new Exception($"netsh command failed: {error}\n{output}");
                }
            }
        }
    }
}
