using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ESPmote
{
    public class ESPClient
    {
        const UInt16 remotePort = 8765;         // fixed
        private UdpClient client = new UdpClient();

        public ESPClient(string ipAddress)
        {
            client.Connect(ipAddress, remotePort);      // UDP "Connects" blocken nicht
        }

        public async Task<bool> Connect()
        {
            byte[] heloData = Encoding.ASCII.GetBytes("HELO");
            client.Send(heloData, heloData.Length);

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            UdpReceiveResult result = await client.ReceiveAsync();
            return (result.Buffer.Length >= 4 && Encoding.ASCII.GetString(result.Buffer).StartsWith("HELO", StringComparison.InvariantCulture));
        }

        public async Task<string> Receive()
        {
            byte[] recvData = Encoding.ASCII.GetBytes("RECV");
            client.Send(recvData, recvData.Length);

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            UdpReceiveResult result = await client.ReceiveAsync();
            return Encoding.ASCII.GetString(result.Buffer);
        }

        public void Send(string code)
        {
            byte[] sendData = Encoding.ASCII.GetBytes("SEND " + code);
            client.Send(sendData, sendData.Length);
        }
    }
}
