using System.Net;
using System.Net.Sockets;

namespace HK.WebApi.Utilities
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// 获取客户端真实 IP 地址，并将 IPv6 映射地址（如 ::ffff:192.168.1.100）转换为纯 IPv4
        /// </summary>
        public static string GetClientIp(this HttpRequest request)
        {
            string ipString = null;

            // 1. 优先从反向代理头获取
            var xff = request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(xff))
            {
                ipString = xff.Split(',').First().Trim();
            }
            else
            {
                // 2. 直接连接：使用远程 IP
                var remoteIp = request.HttpContext.Connection.RemoteIpAddress;
                if (remoteIp != null)
                    ipString = remoteIp.ToString();
            }

            // 如果没有获取到 IP
            if (string.IsNullOrEmpty(ipString))
                return "unknown";

            // 处理 IPv6 映射地址（::ffff:192.168.1.100 → 192.168.1.100）
            if (IPAddress.TryParse(ipString, out IPAddress ip))
            {
                if (ip.AddressFamily == AddressFamily.InterNetworkV6 &&
                    ip.IsIPv4MappedToIPv6)
                {
                    return ip.MapToIPv4().ToString();
                }
            }

            return ipString; // 已是 IPv4 或普通 IPv6
        }
    }
}
