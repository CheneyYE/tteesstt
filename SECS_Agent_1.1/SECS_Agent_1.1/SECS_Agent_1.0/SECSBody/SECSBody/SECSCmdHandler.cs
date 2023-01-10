using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using SECSTool;
using Newtonsoft.Json.Linq;

namespace SECSBody
{
    public class SECSCmdHandler
    {
        public SECSBodyGenerator bodyGenerator;
        public SocketConnector connector;
        JAInterpreter itpr;

        public SECSCmdHandler()
        {
            bodyGenerator = new SECSBodyGenerator();
            connector = new SocketConnector("127.0.0.1", 5566);
            itpr =new JAInterpreter();
        }
        /// <summary>
        /// Process Receieve Msg, if need response, call responseMsg func immediately.
        /// </summary>
        /// <param name="msg"></param>
        //public ArrayList HandleRcvMsg(ArrayList msg)
        //{
        //    switch (msg[0])
        //    {
        //        case "S1F13":
        //            break;
        //        case "S1F3":
        //            break;
        //        case "S6F12":
        //            break;
        //        case "S3F17":
        //            break;
        //    }
        //}

        //public ArrayList ResponseMsg(ArrayList msg)
        //{

        //}
    }
}
