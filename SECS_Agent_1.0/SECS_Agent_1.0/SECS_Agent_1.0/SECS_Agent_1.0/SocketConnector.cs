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
        byte[] result=new byte[1024];
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        public delegate void RcvEQPMsgHandler(object sender, MSGArgument e);
        public event RcvEQPMsgHandler RcvMsgEvent;
        JAInterpreter JA_Intrp;

        public SocketConnector( )
        {
            eqpSocketListen= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            JA_Intrp = new JAInterpreter();
        }

        protected void OnRcvEQPMsgProcess(MSGArgument rcvMsg)
        {
            RcvEQPMsgHandler handler = RcvMsgEvent;
            if (handler != null)
                handler(this, rcvMsg);
        }

        public void StartListen(int port)
        {
            IPEndPoint point = new IPEndPoint(ip, port);
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
                    //readData = "{\r\n  \"SxFx\": \"S6F11\",\r\n  \"LIST\": [\r\n    {\r\n      \"1234\": \"U4\"\r\n    },\r\n    [\r\n      {\r\n        \"20001\": \"U4\"\r\n      },\r\n      [\r\n        {\r\n          \"AAA\": \"A\",\r\n          \"BBB\": \"A\"\r\n        }\r\n      ]\r\n    ]\r\n  ]\r\n}";
                    ArrayList rcvArrayList = JA_Intrp.JSONObj2Array(readData);
                    JObject rcvJObj = JsonConvert.DeserializeObject<JObject>(readData);
                    OnRcvEQPMsgProcess(new MSGArgument(rcvArrayList, rcvJObj)); //Trigger RcvEvent
                }
                catch(Exception ex)
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
        private ArrayList msg;
        private JObject msg_JObj;
        public MSGArgument(ArrayList msg, JObject jobj)
        {
            this.msg = msg;
            this.msg_JObj = jobj;
        }

        public ArrayList Msg
        { get { return msg; } }

        public JObject Msg_JObj
        {
            get { return msg_JObj; }
        }
    }



}
