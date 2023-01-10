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
    struct EventID_Info
    {
        public int dataId;
        public int ceId;
        public int[] rptIdArray;
        public EventID_Info(int data_id,int ce_id, int[] rpt_idArray)
        {
            dataId= data_id;
            ceId= ce_id;
            rptIdArray= rpt_idArray;
        }
    }
    public class SECSBodyGenerator:IMacSECS
    {
        JAInterpreter itpr;
        ArrayList BodyArray;
        Dictionary<LPAEvent, EventID_Info> LPA_EventID_Info_dic;
        Dictionary<LPBEvent, EventID_Info> LPB_EventID_Info_dic;
        Dictionary<MTEvent, EventID_Info> MT_EventID_Info_dic;
        Dictionary<ICEvent, EventID_Info> IC_EventID_Info_dic;
        Dictionary<BTEvent, EventID_Info> BT_EventID_Info_dic;
        Dictionary<OSEvent, EventID_Info> OS_EventID_Info_dic;
        Dictionary<CLEvent, EventID_Info> CL_EventID_Info_dic;
        Dictionary<RREvent, EventID_Info> RR_EventID_Info_dic;
        Dictionary<CBEvent, EventID_Info> CB_EventID_Info_dic;
        Dictionary<EQPEvent, EventID_Info> EQP_EventID_Info_dic;
        Dictionary<SVID, object> SVTable; //Initial by externel
        Dictionary<int, List<SVID>> RPTSV_Mapping_Dic;

        public SECSBodyGenerator()
        {
            itpr=new JAInterpreter();
            BodyArray = new ArrayList();
            InitialEventIDInfo();
            InitialRPTSV_MappingTable();
            //For test
            Dictionary<SVID,object> testSVTable=new Dictionary<SVID,object>
            {
                { SVID.LPA_Pressent, true },
                { SVID.LPA_Placement, true },
                { SVID.LPA_Clamped, true },
                { SVID.LPA_PODID, "PODID_TEST" },
                { SVID.LPA_RFID, "RFID_TEST" },
                { SVID.EQP_Model,"MaskTool"},
                { SVID.EQP_SoftVersion,"V1.0.0"}
            };
            UpdateSVTable(testSVTable);
            //
        }
        #region Initial CE/RPT/SV func
        private void InitialRPTSV_MappingTable()
        {
            RPTSV_Mapping_Dic = new Dictionary<int, List<SVID>>();
            //EQP RPT&SVID Mapping
            RPTSV_Mapping_Dic.Add(100, new List<SVID>{SVID.EQP_Model,SVID.EQP_SoftVersion,SVID.EQP_Time});
            RPTSV_Mapping_Dic.Add(102, new List<SVID> { SVID.EQP_CommState, SVID.EQP_Mode});
            RPTSV_Mapping_Dic.Add(103, new List<SVID> { SVID.EQP_FanSpeed, SVID.EQP_ExhaustSpeed});
            RPTSV_Mapping_Dic.Add(104, new List<SVID> { SVID.EQP_CurrMaskID, SVID.EQP_CurrPrJobID,SVID.EQP_CurrCtrlJobID });
            RPTSV_Mapping_Dic.Add(105, new List<SVID> { SVID.EQP_CurrMaskID, SVID.EQP_ProcessResult });

            //LPA&B RPT&SVID Mapping
            RPTSV_Mapping_Dic.Add(201, new List<SVID> { SVID.LPA_Pressent, SVID.LPA_Placement, SVID.LPA_Clamped });
            RPTSV_Mapping_Dic.Add(202, new List<SVID> { SVID.LPA_PODID, SVID.LPA_RFID });
            RPTSV_Mapping_Dic.Add(211, new List<SVID> { SVID.LPB_Pressent, SVID.LPB_Placement, SVID.LPB_Clamped });
            RPTSV_Mapping_Dic.Add(212, new List<SVID> { SVID.LPB_PODID, SVID.LPB_RFID });

            ////MT RPT&SVID Mapping
            //RPTSV_Mapping_Dic.Add(300001, new List<SVID> { SVID.MT_RobotPosition, SVID.MT_ClawState, SVID.MT_ReticleExist });
            //RPTSV_Mapping_Dic.Add(300101, new List<SVID> { SVID.MT_RobotSpeed, SVID.MT_OpticalRuler });

            ////IC RPT&SVID Mapping
            //RPTSV_Mapping_Dic.Add(400001, new List<SVID> { SVID.IC_InspectState});
            //RPTSV_Mapping_Dic.Add(400101, new List<SVID> { SVID.IC_CoorStageXY, SVID.IC_CoorStageZ,SVID.IC_RotateAngle });

            ////OS RPT&SVID Mapping
            //RPTSV_Mapping_Dic.Add(500001, new List<SVID> { SVID.OS_OpenStageState,SVID.OS_BoxExist,SVID.OS_SonicSensor });

            ////BT RPT&SVID Mapping
            //RPTSV_Mapping_Dic.Add(700001, new List<SVID> { SVID.BT_RobotPosition, SVID.BT_ClawState, SVID.BT_BoxExist });
            //RPTSV_Mapping_Dic.Add(700101, new List<SVID> { SVID.BT_RobotSpeed, SVID.BT_ClawDistance });

            ////Cabinet RPT&SVID Mapping
            //RPTSV_Mapping_Dic.Add(800001, new List<SVID> { SVID.CB_RasterState, SVID.CB_DrawerState});
            //RPTSV_Mapping_Dic.Add(800101, new List<SVID> {
            //    SVID.CB_BoxID_01,
            //    SVID.CB_BoxID_02,
            //    SVID.CB_BoxID_03,
            //    SVID.CB_BoxID_04,
            //    SVID.CB_BoxID_05,
            //    SVID.CB_BoxID_06,
            //    SVID.CB_BoxID_07,
            //    SVID.CB_BoxID_08,
            //    SVID.CB_BoxID_09,
            //    SVID.CB_BoxID_10,
            //    SVID.CB_BoxID_11,
            //    SVID.CB_BoxID_12,
            //    SVID.CB_BoxID_13,
            //    SVID.CB_BoxID_14,
            //    SVID.CB_BoxID_15,
            //    SVID.CB_BoxID_16,
            //    SVID.CB_BoxID_17,
            //    SVID.CB_BoxID_18,
            //    SVID.CB_BoxID_19,
            //    SVID.CB_BoxID_20,
            //    SVID.CB_BoxID_21,
            //    SVID.CB_BoxID_22,
            //    SVID.CB_BoxID_23,
            //    SVID.CB_BoxID_24,
            //    SVID.CB_BoxID_25,
            //    SVID.CB_BoxID_26,
            //    SVID.CB_BoxID_27,
            //    SVID.CB_BoxID_28,
            //    SVID.CB_BoxID_29,
            //    SVID.CB_BoxID_30,
            //    SVID.CB_BoxID_31,
            //    SVID.CB_BoxID_32,
            //    SVID.CB_BoxID_33,
            //    SVID.CB_BoxID_34,
            //    SVID.CB_BoxID_35
            //});

            ////RR RPT&SVID Mapping
            //RPTSV_Mapping_Dic.Add(900001, new List<SVID> { SVID.RR_ReadBarcode });
            //RPTSV_Mapping_Dic.Add(900101, new List<SVID> { SVID.RR_LaserFront, SVID.RR_LaserSide });
        }
        private void InitialEventIDInfo()
        {
            LPA_EventID_Info_dic = new Dictionary<LPAEvent, EventID_Info>();
            LPA_EventID_Info_dic.Add(LPAEvent.InitialComplete, new EventID_Info(201,201,new int[] { 201}));
            LPA_EventID_Info_dic.Add(LPAEvent.Idel, new EventID_Info(202, 202, new int[] { 201 }));
            LPA_EventID_Info_dic.Add(LPAEvent.LoadComplete, new EventID_Info(203, 203, new int[] { 201 }));
            LPA_EventID_Info_dic.Add(LPAEvent.ReadPODID, new EventID_Info(204, 204, new int[] { 202 }));
            LPA_EventID_Info_dic.Add(LPAEvent.DockComplete, new EventID_Info(205, 205, new int[] { 202 }));
            LPA_EventID_Info_dic.Add(LPAEvent.UndockComplete, new EventID_Info(206, 206, new int[] { 202 }));
            LPA_EventID_Info_dic.Add(LPAEvent.UnLoadComplete, new EventID_Info(207, 207, new int[] { 201 }));

            //LPB_EventID_Info_dic = new Dictionary<LPBEvent, EventID_Info>();
            //LPB_EventID_Info_dic.Add(LPBEvent.InitialComplete, new EventID_Info(201, 210001, new int[] { 210001 }));
            //LPB_EventID_Info_dic.Add(LPBEvent.Idel, new EventID_Info(202, 210101, new int[] { 210001 }));
            //LPB_EventID_Info_dic.Add(LPBEvent.LoadComplete, new EventID_Info(203, 210102, new int[] { 210001 }));
            //LPB_EventID_Info_dic.Add(LPBEvent.ReadPODID, new EventID_Info(204, 210103, new int[] { 210101 }));
            //LPB_EventID_Info_dic.Add(LPBEvent.DockComplete, new EventID_Info(205, 210104, new int[] { 210101 }));
            //LPB_EventID_Info_dic.Add(LPBEvent.UndockComplete, new EventID_Info(206, 210105, new int[] { 210101 }));
            //LPB_EventID_Info_dic.Add(LPBEvent.UnLoadComplete, new EventID_Info(207, 210106, new int[] { 210001 }));

            //MT_EventID_Info_dic = new Dictionary<MTEvent, EventID_Info>();
            //MT_EventID_Info_dic.Add(MTEvent.InitialComplete, new EventID_Info(301, 300001, new int[] { 300001, 300101 }));
            //MT_EventID_Info_dic.Add(MTEvent.ArriveHome, new EventID_Info(302, 300101, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ArriveLPA, new EventID_Info(303, 300102, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ArriveLPB, new EventID_Info(304, 300103, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ArriveRecognizer, new EventID_Info(305, 300104, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ArriveInspection, new EventID_Info(306, 300105, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ArriveClean, new EventID_Info(307, 300106, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ArriveOpenStage, new EventID_Info(308, 300107, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ClawReleased, new EventID_Info(309, 300201, new int[] { 300001 }));
            //MT_EventID_Info_dic.Add(MTEvent.ClawClamped, new EventID_Info(310, 300202, new int[] { 300001 }));

            //IC_EventID_Info_dic = new Dictionary<ICEvent, EventID_Info>();
            //IC_EventID_Info_dic.Add(ICEvent.InitialComplete, new EventID_Info(401, 400001, new int[] { 400001, 400101 }));
            //IC_EventID_Info_dic.Add(ICEvent.InspectStart, new EventID_Info(402, 400101, new int[] { 400001}));
            //IC_EventID_Info_dic.Add(ICEvent.InspectGlassComplete, new EventID_Info(403, 400102, new int[] { 400001 }));
            //IC_EventID_Info_dic.Add(ICEvent.InspectPellicleComplete, new EventID_Info(404, 400103, new int[] { 400001 }));
            //IC_EventID_Info_dic.Add(ICEvent.InspectReticleSideComplete, new EventID_Info(405, 400104, new int[] { 400001 }));
            //IC_EventID_Info_dic.Add(ICEvent.InspectComplete, new EventID_Info(406, 400105, new int[] { 400001 }));

            //CL_EventID_Info_dic = new Dictionary<CLEvent, EventID_Info>();
            //CL_EventID_Info_dic.Add(CLEvent.InitialComplete, new EventID_Info(501, 500001, new int[] { 500001}));
            //CL_EventID_Info_dic.Add(CLEvent.CleanStart, new EventID_Info(502, 500101, new int[] { 500001 }));
            //CL_EventID_Info_dic.Add(CLEvent.CleanComplete, new EventID_Info(503, 500102, new int[] { 500001 }));

            //OS_EventID_Info_dic = new Dictionary<OSEvent, EventID_Info>();
            //OS_EventID_Info_dic.Add(OSEvent.InitialComplete, new EventID_Info(601, 600001, new int[] { 600001 }));
            //OS_EventID_Info_dic.Add(OSEvent.BoxLoadComplete, new EventID_Info(602, 600101, new int[] { 600001 }));
            //OS_EventID_Info_dic.Add(OSEvent.BoxOpenComplete, new EventID_Info(603, 600102, new int[] { 600001 }));
            //OS_EventID_Info_dic.Add(OSEvent.BoxCloseComplete, new EventID_Info(604, 600103, new int[] { 600001 }));
            //OS_EventID_Info_dic.Add(OSEvent.BoxUnloadComplete, new EventID_Info(605, 600104, new int[] { 600001 }));

            //BT_EventID_Info_dic = new Dictionary<BTEvent, EventID_Info>();
            //BT_EventID_Info_dic.Add(BTEvent.InitialComplete, new EventID_Info(701, 700001, new int[] { 700001, 700101 }));
            //BT_EventID_Info_dic.Add(BTEvent.ArriveHome, new EventID_Info(702, 700101, new int[] { 700001}));
            //BT_EventID_Info_dic.Add(BTEvent.ArriveCabinet, new EventID_Info(703, 700102, new int[] { 700001 }));
            //BT_EventID_Info_dic.Add(BTEvent.ArriveOpenStage, new EventID_Info(704, 700103, new int[] { 700001 }));
            //BT_EventID_Info_dic.Add(BTEvent.ClawReleased, new EventID_Info(705, 700104, new int[] { 700001 }));
            //BT_EventID_Info_dic.Add(BTEvent.ClawClamped, new EventID_Info(706, 700105, new int[] { 700001 }));

            //CB_EventID_Info_dic = new Dictionary<CBEvent, EventID_Info>();
            //CB_EventID_Info_dic.Add(CBEvent.InitialComplete, new EventID_Info(801, 800001, new int[] { 800001,800101}));
            //CB_EventID_Info_dic.Add(CBEvent.UnitLoadComplete, new EventID_Info(802, 800101, new int[] { 800001 }));
            //CB_EventID_Info_dic.Add(CBEvent.BatchDockComplete, new EventID_Info(802, 800101, new int[] { 800101 }));
            //CB_EventID_Info_dic.Add(CBEvent.BatchUndockComplete, new EventID_Info(802, 800101, new int[] { 800101 }));
            //CB_EventID_Info_dic.Add(CBEvent.BatchUnloadComplete, new EventID_Info(802, 800101, new int[] { 800001 }));

            //RR_EventID_Info_dic = new Dictionary<RREvent, EventID_Info>();
            //RR_EventID_Info_dic.Add(RREvent.InitialComplete, new EventID_Info(901, 900001, new int[] { 900101 }));
            //RR_EventID_Info_dic.Add(RREvent.BarcodeReadComplete, new EventID_Info(902, 900101, new int[] { 900001 }));
            //RR_EventID_Info_dic.Add(RREvent.ReticleClawCalibrateComplete, new EventID_Info(803, 900201, new int[] { 900101 }));

            EQP_EventID_Info_dic = new Dictionary<EQPEvent, EventID_Info>();
            EQP_EventID_Info_dic.Add(EQPEvent.PowerOn, new EventID_Info(101, 101, new int[] { 101 }));
            EQP_EventID_Info_dic.Add(EQPEvent.ModeChanged, new EventID_Info(102, 102, new int[] { 102 }));
            //EQP_EventID_Info_dic.Add(EQPEvent.CreateJobComplete, new EventID_Info(1001, 1000101, new int[] { 1000001 }));
            //EQP_EventID_Info_dic.Add(EQPEvent.ProcessStart, new EventID_Info(1002, 1000102, new int[] { 1000001 }));
            //EQP_EventID_Info_dic.Add(EQPEvent.ProcessEnd, new EventID_Info(1003, 1000103, new int[] { 1000001 }));
            //EQP_EventID_Info_dic.Add(EQPEvent.BatchBoxProcessComplete, new EventID_Info(1004, 1000104, new int[] { 800101 }));
        }
        #endregion
        #region Utility Function
        JObject GenBody_S6F11(int dataId,int ceId, ArrayList rtpArray)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(dataId);
                BodyArray.Add(ceId);
                BodyArray.Add(rtpArray);
                return itpr.Array2JSONObj(6, 11, BodyArray);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        ArrayList GenRptArrayList_S6F11(int[] rptIDArray)
        {
            try
            {
                ArrayList rptArrayList = new ArrayList();
                foreach (int rptId in rptIDArray)
                {
                    ArrayList rptArray = new ArrayList();
                    rptArray.Add(rptId);
                    ArrayList SVArray = new ArrayList();
                    foreach (var tmpSVID in RPTSV_Mapping_Dic[rptId])
                    {
                        SVArray.Add(SVTable[tmpSVID]);
                    }
                    rptArray.Add(SVArray);
                    rptArrayList.Add(rptArray);
                }
                return rptArrayList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Initial or Update SVTable
        /// </summary>
        /// <param name="newSVTable"></param>
        public void UpdateSVTable(Dictionary<SVID, object> newSVTable)
        {
            SVTable = newSVTable;
        } 
        #endregion
        #region S6Fx
        public JObject GenBody_S6F11_EQP(EQPEvent CEName)
        {
            try
            {
                int dataID = EQP_EventID_Info_dic[CEName].dataId;
                int ceID = EQP_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = EQP_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S6F11_LPA(LPAEvent CEName)
        {
            try
            {
                int dataID = LPA_EventID_Info_dic[CEName].dataId;
                int ceID = LPA_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = LPA_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_LPB(LPBEvent CEName)
        {
            try
            {
                int dataID = LPB_EventID_Info_dic[CEName].dataId;
                int ceID = LPB_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = LPB_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_IC(ICEvent CEName)
        {
            try
            {
                int dataID = IC_EventID_Info_dic[CEName].dataId;
                int ceID = IC_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = IC_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_MT(MTEvent CEName)
        {
            try
            {
                int dataID = MT_EventID_Info_dic[CEName].dataId;
                int ceID = MT_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = MT_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_BT(BTEvent CEName)
        {
            try
            {
                int dataID = BT_EventID_Info_dic[CEName].dataId;
                int ceID = BT_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = BT_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_CL(CLEvent CEName)
        {
            try
            {
                int dataID = CL_EventID_Info_dic[CEName].dataId;
                int ceID = CL_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = CL_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_CB(CBEvent CEName)
        {
            try
            {
                int dataID = CB_EventID_Info_dic[CEName].dataId;
                int ceID = CB_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = CB_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_RR(RREvent CEName)
        {
            try
            {
                int dataID = RR_EventID_Info_dic[CEName].dataId;
                int ceID = RR_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = RR_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S6F11_OS(OSEvent CEName)
        {
            try
            {
                int dataID = OS_EventID_Info_dic[CEName].dataId;
                int ceID = OS_EventID_Info_dic[CEName].ceId;
                int[] rptIDArray = OS_EventID_Info_dic[CEName].rptIdArray;
                JObject jObj_msg = GenBody_S6F11(dataID, ceID, GenRptArrayList_S6F11(rptIDArray));
                return jObj_msg;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region S1Fx
        public JObject GenBody_S1F2()
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(SVTable[SVID.EQP_Model]);
                BodyArray.Add(SVTable[SVID.EQP_SoftVersion]);
                return itpr.Array2JSONObj(1, 2, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S1F4(List<SVID> SVList)
        {
            try
            {
                BodyArray.Clear();
                foreach(SVID svid in SVList)
                {
                    BodyArray.Add(SVTable[svid]);
                }
                return itpr.Array2JSONObj(1, 4, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S1F13()
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(SVTable[SVID.EQP_Model]);
                BodyArray.Add(SVTable[SVID.EQP_SoftVersion]);
                return itpr.Array2JSONObj(1, 13, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S1F14()
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add((byte)0x00);
                ArrayList tmpArrayList = new ArrayList { SVTable[SVID.EQP_Model], SVTable[SVID.EQP_SoftVersion] };
                BodyArray.Add(tmpArrayList);
                return itpr.Array2JSONObj(1, 14, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S1F18(S1F18_OnlineReqAck ack)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(Convert.ToByte(ack));
                return itpr.Array2JSONObj(1, 18, BodyArray);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region S2Fx
        public JObject GenBody_S2F34()
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(Convert.ToByte(0));
                return itpr.Array2JSONObj(2, 36, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S2F36()
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(Convert.ToByte(0));
                return itpr.Array2JSONObj(2, 36, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GenBody_S2F38(S2F38_EventReportSwitch ack)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(Convert.ToByte(ack));
                return itpr.Array2JSONObj(2, 38, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region S3Fx
        //S3F18: CAACK is 0~4 need to refer the SECS Manual.
        public JObject GenBody_S3F18(byte caACK,Dictionary<int,string> errorInfo_dic)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(caACK);
                ArrayList tmpArray=new ArrayList();
                foreach(var errorInfo in errorInfo_dic)
                {
                    ArrayList tmpErrorInfoArray = new ArrayList { errorInfo.Key,errorInfo.Value};
                    tmpArray.Add(tmpErrorInfoArray);
                }
                BodyArray.Add(tmpArray);
                return itpr.Array2JSONObj(3, 18, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S3F28()
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(Convert.ToUInt32(0));
                BodyArray.Add(new ArrayList());
                return itpr.Array2JSONObj(3, 28, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S3F18(byte caACK)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(caACK);
                BodyArray.Add(new ArrayList());
                return itpr.Array2JSONObj(3, 18, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region S7Fx
        public JObject GenBody_S7F20(List<string> rcpList)
        {
            try
            {
                BodyArray.Clear();
                foreach(string rcp in rcpList)
                {
                    BodyArray.Add(rcp);
                }
                return itpr.Array2JSONObj(7, 20, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
        #region S14Fx
        public JObject GenBody_S14F10(string objSpec, Dictionary<int, string> attributeInfo_dic,Dictionary<int,string> errorInfo_dic)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(objSpec);
                ArrayList tmpArray = new ArrayList();
                foreach (var attributeInfo in attributeInfo_dic)
                {
                    ArrayList tmpAttributeInfoArray = new ArrayList { attributeInfo.Key, attributeInfo.Value };
                    tmpArray.Add(tmpAttributeInfoArray);
                }
                BodyArray.Add(tmpArray);
                ArrayList tmpArray2 = new ArrayList { (byte)0 };//OBJACK:0 is success
                foreach (var errorInfo in errorInfo_dic)
                {
                    ArrayList tmpErrorInfoArray = new ArrayList { errorInfo.Key, errorInfo.Value };
                    tmpArray2.Add(tmpErrorInfoArray);
                }
                BodyArray.Add(tmpArray2);
                return itpr.Array2JSONObj(4, 10, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S14F10(string objSpec,Dictionary<int,string> attributeInfo_dic)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(objSpec);
                ArrayList tmpArray = new ArrayList();
                foreach (var attributeInfo in attributeInfo_dic)
                {
                    ArrayList tmpattributeInfoArray = new ArrayList { attributeInfo.Key, attributeInfo.Value };
                    tmpArray.Add(tmpattributeInfoArray);
                }
                BodyArray.Add(tmpArray);
                BodyArray.Add(new ArrayList { (byte)0, new ArrayList() }); //OBJACK:0 is success
                return itpr.Array2JSONObj(4, 10, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JObject GenBody_S14F10(string objSpec)
        {
            try
            {
                BodyArray.Clear();
                BodyArray.Add(objSpec);
                BodyArray.Add(new ArrayList());
                BodyArray.Add(new ArrayList { (byte)0, new ArrayList() }); //OBJACK:0 is success
                return itpr.Array2JSONObj(4, 10, BodyArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
