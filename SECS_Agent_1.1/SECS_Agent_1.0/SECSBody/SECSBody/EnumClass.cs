using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECSBody
{
    public enum LPName
    {
        A,
        B,
    }
    public enum LPAEvent
    {
        InitialComplete,
        Idel,
        LoadComplete,
        ReadPODID,
        DockComplete,
        UndockComplete,
        UnLoadComplete,
    }

    public enum LPBEvent
    {
        InitialComplete,
        Idel,
        LoadComplete,
        ReadPODID,
        DockComplete,
        UndockComplete,
        UnLoadComplete,
    }

    public enum MTEvent
    {
        InitialComplete,
        ArriveHome,
        ArriveLPA,
        ArriveLPB,
        ArriveRecognizer,
        ArriveInspection,
        ArriveClean,
        ArriveOpenStage,
        ClawReleased,
        ClawClamped,
    }

    public enum ICEvent
    {
        InitialComplete,
        InspectStart,
        InspectGlassComplete,
        InspectPellicleComplete,
        InspectReticleSideComplete,
        InspectComplete,
    }

    public enum CLEvent
    {
        InitialComplete,
        CleanStart,
        CleanComplete,
    }

    public enum OSEvent
    {
        InitialComplete,
        BoxLoadComplete,
        BoxOpenComplete,
        BoxCloseComplete,
        BoxUnloadComplete,
    }

    public enum BTEvent
    {
        InitialComplete,
        ArriveHome,
        ArriveCabinet,
        ArriveOpenStage,
        ClawReleased,
        ClawClamped,
    }

    public enum RREvent
    {
        InitialComplete,
        BarcodeReadComplete,
        ReticleClawCalibrateComplete,
    }

    public enum CBEvent
    {
        InitialComplete,
        UnitLoadComplete,
        BatchDockComplete,
        BatchUndockComplete,
        BatchUnloadComplete,
    }

    public enum EQPEvent
    {
        PowerOn,
        ModeChanged,
        CreateJobComplete,
        ProcessStart,
        ProcessEnd,
        BatchBoxProcessComplete,
    }

    public enum SVID
    {
        EQP_Model=100001,
        EQP_SoftVersion=100002,
        EQP_Time=100003,
        EQP_CommState=100101,
        EQP_Mode=100102,
        EQP_FanSpeed=100201,
        EQP_ExhaustSpeed=100202,
        EQP_CurrMaskID=1000001,
        EQP_CurrPrJobID=1000002,
        EQP_CurrCtrlJobID=1000003,
        EQP_ProcessResult=1000004,
        LPA_Pressent=200001,
        LPA_Placement = 200002,
        LPA_Clamped = 200003,
        LPA_PODID = 200101,
        LPA_RFID = 200102,
        LPB_Pressent = 210001,
        LPB_Placement = 210002,
        LPB_Clamped = 210003,
        LPB_PODID = 210101,
        LPB_RFID = 210102,
        MT_RobotPosition=300001,
        MT_ClawState=300002,
        MT_ReticleExist=300003,
        MT_RobotSpeed=300101,
        MT_OpticalRuler=300102,
        IC_InspectState=400001,
        IC_CoorStageXY=400101,
        IC_CoorStageZ=400102,
        IC_RotateAngle=400103,
        CL_AirPurgeState=500001,
        CL_RasterState=500002,
        CL_AirGunPressureLimit=500003,
        OS_OpenStageState=600001,
        OS_BoxExist=600002,
        OS_SonicSensor=600003,
        BT_RobotPosition = 700001,
        BT_ClawState = 700002,
        BT_BoxExist = 700003,
        BT_RobotSpeed = 700101,
        BT_ClawDistance = 700102,
        CB_RasterState=800001,
        CB_DrawerState = 800002,
        CB_BoxID_01 = 800101,
        CB_BoxID_02 = 800102,
        CB_BoxID_03 = 800103,
        CB_BoxID_04 = 800104,
        CB_BoxID_05 = 800105,
        CB_BoxID_06 = 800106,
        CB_BoxID_07 = 800107,
        CB_BoxID_08 = 800108,
        CB_BoxID_09 = 800109,
        CB_BoxID_10 = 800110,
        CB_BoxID_11 = 800111,
        CB_BoxID_12 = 800112,
        CB_BoxID_13 = 800113,
        CB_BoxID_14 = 800114,
        CB_BoxID_15 = 800115,
        CB_BoxID_16 = 800116,
        CB_BoxID_17 = 800117,
        CB_BoxID_18 = 800118,
        CB_BoxID_19 = 800119,
        CB_BoxID_20 = 800120,
        CB_BoxID_21 = 800121,
        CB_BoxID_22 = 800122,
        CB_BoxID_23 = 800123,
        CB_BoxID_24 = 800124,
        CB_BoxID_25 = 800125,
        CB_BoxID_26 = 800126,
        CB_BoxID_27 = 800127,
        CB_BoxID_28 = 800128,
        CB_BoxID_29 = 800129,
        CB_BoxID_30 = 800130,
        CB_BoxID_31 = 800131,
        CB_BoxID_32 = 800132,
        CB_BoxID_33 = 800133,
        CB_BoxID_34 = 800134,
        CB_BoxID_35 = 800135,
        RR_ReadBarcode=900001,
        RR_LaserFront=900101,
        RR_LaserSide=900102,
    }

    public enum S1F18_OnlineReqAck
    {
        OnlineAccept=0x00,
        OnlineNotAllowed=0x01,
        EQPAlreadOnline=0x02,
    }

    public enum S2F38_EventReportSwitch
    {
        Accepted=0x00,
        Denied_CEIDNotExist=0x01,
    }
}
