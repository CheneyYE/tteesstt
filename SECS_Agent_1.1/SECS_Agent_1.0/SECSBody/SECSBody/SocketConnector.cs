using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SECSBody
{
    public class SocketConnector
    {
        byte[] bytes=new byte[1024];
        Socket client;
        public SocketConnector(string ip, int port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        }


        public void SendMsg(JObject jObj)
        {
            string dataToSend = JsonConvert.SerializeObject(jObj);
            byte[] dataBytes = Encoding.Default.GetBytes(dataToSend);
            client.Send(dataBytes);
        }
    }
}
