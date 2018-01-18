using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ESPmote
{
    public static class ESPClient
    {
        const UInt16 remotePort = 8765;         // fixed
        private static UdpClient client = new UdpClient();

        public static async Task<bool> Connect(string ipAddress)
        {
#if DEBUG
            // Demonstration zur Veranschaulichung der asynchronen Aufrufe
            await Task.Delay(1000);
#endif

            try
            {
                client.Connect(ipAddress, remotePort);      // UDP "Connects" blocken nicht

                byte[] heloData = Encoding.ASCII.GetBytes("HELO");
                client.Send(heloData, heloData.Length);

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                UdpReceiveResult result = await client.ReceiveAsync();
                return (result.Buffer.Length >= 4 && Encoding.ASCII.GetString(result.Buffer).StartsWith("HELO", StringComparison.InvariantCulture));
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message;
                return false;
            }
        }

        public static async Task<string> Receive()
        {
            try
            {
                byte[] recvData = Encoding.ASCII.GetBytes("RECV");
                client.Send(recvData, recvData.Length);

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                UdpReceiveResult result = await client.ReceiveAsync();

                string code = Encoding.ASCII.GetString(result.Buffer);
                if (code == "0")
                {
                    LastErrorMessage = "Konnte Code nicht einlesen!";
                    return null;
                }
                return code;
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message;
                return null;
            }
        }

        public static bool Send(string code)
        {
            try
            {
                byte[] sendData = Encoding.ASCII.GetBytes("SEND " + code);
                client.Send(sendData, sendData.Length);
                return true;
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message;
                return false;
            }
        }

        public static string LastErrorMessage { get; private set; }
    }
}
