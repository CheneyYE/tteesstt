using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using SECSTool;

namespace SECS_Agent_1._0
{
    public class SocketConnector
    {
        Thread recevieThread;
        Thread connectThread;
        public Socket eqpSocketListen;
        public Socket eqpSocketConnect;
        byte[] result = new byte[1024];
        //IPAddress ip = IPAddress.Parse("127.0.0.1");
        public delegate void RcvEQPMsgHandler(object sender, MSGArgument e);
        public event RcvEQPMsgHandler RcvMsgEvent;

        public SocketConnector()
        {
            eqpSocketListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected void OnRcvEQPMsgProcess(MSGArgument rcvMsg)
        {
            RcvEQPMsgHandler handler = RcvMsgEvent;
            if (handler != null)
                handler(this, rcvMsg);
        }

        public void StartListen(int port)
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, port);
            eqpSocketListen.Bind(point);
            eqpSocketListen.Listen(10);
            connectThread = new Thread(ListenSocket);
            connectThread.Start();
        }
        /// <summary>
        /// 監聽客戶端連線
        /// </summary>
        void ListenSocket()
        {
            while (true)
            {
                eqpSocketConnect = eqpSocketListen.Accept();
                recevieThread = new Thread(ReceiveMessage);//建立一個接受資訊的程序
                recevieThread.Start();//執行新的Socket接受資訊
            }
        }

        /// <summary>
        ///  接受Socket訊息
        /// </summary>
        /// <param name="clientSocket"></param>
        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    //通過clientsocket接收資料
                    int num = eqpSocketConnect.Receive(result);//把接受到的資料存到bytes陣列中並賦值給num
                    Thread.Sleep(1000);    //等待1秒鐘
                    string readData = Encoding.UTF8.GetString(result);
                    //For Test
                    readData = "{\r\n  \"SxFx\": \"S6F12\",\r\n  \"SecsMsg\": [\r\n    {\r\n      \"B\": \"0x00\"\r\n    },\r\n    {\r\n      \"B\": \"0x00\"\r\n    },\r\n    [\r\n      { \"A\": \"AAA\" },\r\n      [\r\n        { \"A\": \"AAA\" }\r\n      ]\r\n    ]\r\n  ]\r\n}";
                    //readData = "{\r\n  \"SxFx\": \"S3F17\",\r\n  \"SecsMsg\": [\r\n  ]\r\n}";
                    //readData = "{\r\n  \"SxFx\": \"S3F17\",\r\n  \"SecsMsg\": {\"B\": \"0x00\"}\r\n}";
                    //readData = "{\r\n  \"SxFx\": \"S3F17\",\r\n  \"SecsMsg\": [\r\n    {\r\n      \"B\": \"0x00\"\r\n    }\r\n  ]\r\n}";
                    //readData = "{\r\n  \"SxFx\": \"S3F17\"\r\n}";

                    JObject rcvJObj = JsonConvert.DeserializeObject<JObject>(readData);
                    OnRcvEQPMsgProcess(new MSGArgument(rcvJObj)); //Trigger RcvEvent
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void DisConnect()
        {
            eqpSocketConnect.Close();
        }
    }

    public class MSGArgument : EventArgs
    {
        private JObject msg_JObj;
        public MSGArgument(JObject jobj)
        {
            this.msg_JObj = jobj;
        }

        public JObject Msg_JObj
        {
            get { return msg_JObj; }
        }
    }
}
