using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace SECSBody
{
    public interface IMacSECS
    {
        JObject GenBody_S6F11_EQP(EQPEvent CEName);
        JObject GenBody_S6F11_LPA(LPAEvent CEName);
        JObject GenBody_S6F11_LPB(LPBEvent CEName);
        JObject GenBody_S6F11_MT(MTEvent CEName);
        JObject GenBody_S6F11_IC(ICEvent CEName);
        JObject GenBody_S6F11_BT(BTEvent CEName);
        JObject GenBody_S6F11_OS(OSEvent CEName);
        JObject GenBody_S6F11_CL(CLEvent CEName);
        JObject GenBody_S6F11_CB(CBEvent CEName);
        JObject GenBody_S6F11_RR(RREvent CEName);
    }
}
