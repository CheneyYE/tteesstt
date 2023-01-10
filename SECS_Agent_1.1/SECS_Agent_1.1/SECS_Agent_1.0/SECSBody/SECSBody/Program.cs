using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace SECSBody
{
    public class Program
    {
        static void Main(string[] args)
        {
            JObject sendedJObj = null;
            SECSCmdHandler handler = new SECSCmdHandler();

            //Initial
            //sendedJObj = handler.bodyGenerator.GenBody_S1F18(S1F18_OnlineReqAck.OnlineAccept);
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S2F38(S2F38_EventReportSwitch.Accepted);
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S2F36();
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S2F34();
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S2F34();
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S2F36();
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S2F38(S2F38_EventReportSwitch.Accepted);
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S1F14();
            //handler.connector.SendMsg(sendedJObj);

            //sendedJObj = handler.bodyGenerator.GenBody_S3F28();
            //handler.connector.SendMsg(sendedJObj);

            //List<string> rcpList = new List<string>() { "rcp1","rcp2","rcp3"};
            //sendedJObj = handler.bodyGenerator.GenBody_S7F20(rcpList);
            //handler.connector.SendMsg(sendedJObj);


            //OCAP
            //sendedJObj = handler.bodyGenerator.GenBody_S6F11_LPA(LPAEvent.Idel);
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S6F11_LPA(LPAEvent.LoadComplete);
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S6F11_LPA(LPAEvent.ReadPODID);
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S3F18(0x00);
            //handler.connector.SendMsg(sendedJObj);
            sendedJObj = handler.bodyGenerator.GenBody_S6F11_LPA(LPAEvent.DockComplete);
            handler.connector.SendMsg(sendedJObj);
            sendedJObj = handler.bodyGenerator.GenBody_S6F11_EQP(EQPEvent.ProcessStart);
            handler.connector.SendMsg(sendedJObj);
            sendedJObj = handler.bodyGenerator.GenBody_S6F11_EQP(EQPEvent.ProcessEnd);
            handler.connector.SendMsg(sendedJObj);
            sendedJObj = handler.bodyGenerator.GenBody_S6F11_LPA(LPAEvent.UndockComplete);
            handler.connector.SendMsg(sendedJObj);
            sendedJObj = handler.bodyGenerator.GenBody_S6F11_LPA(LPAEvent.UnLoadComplete);
            handler.connector.SendMsg(sendedJObj);
            sendedJObj = handler.bodyGenerator.GenBody_S6F11_LPA(LPAEvent.Idel);
            handler.connector.SendMsg(sendedJObj);



            //sendedJObj = handler.bodyGenerator.GenBody_S1F2();
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S1F4(new List<SVID> { SVID.LPA_PODID, SVID.LPA_RFID, SVID.EQP_Model, SVID.EQP_SoftVersion });
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S14F10("TEST", new Dictionary<int, string>(){
            //    {111,"T"},
            //    {222,"E"},
            //    {333,"S"},
            //    {444,"T"},
            //}, new Dictionary<int, string>(){
            //    {555,"Error1"},
            //    {666,"Error2"},
            //    {777,"Error3"},
            //});
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S3F18(1, new Dictionary<int, string>(){
            //    {555,"Error1"},
            //    {666,"Error2"},
            //    {777,"Error3"},
            //});
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S1F13();
            //handler.connector.SendMsg(sendedJObj);
            //sendedJObj = handler.bodyGenerator.GenBody_S1F14();
            //handler.connector.SendMsg(sendedJObj);
        }

    }
}
