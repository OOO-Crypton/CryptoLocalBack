using System.Net;
using System.Net.Sockets;

namespace CryptoLocalBack.Extensions
{
    public class TcpConnectorExtension
    {
        public static async Task Connect()
        {
            TcpClient tcpClient = new();
            await tcpClient.ConnectAsync(IPAddress.Any, 44444);
            NetworkStream stream = tcpClient.GetStream();

        }
    }
}
