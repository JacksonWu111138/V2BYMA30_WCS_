namespace Mirle.STKC.R46YP320.Model.Define
{
    public static class ErrorCode
    {
        public static readonly int Success = 0;
        public static readonly int Initial = 100001;
        public static readonly int Exception = 100002;

        //For clsDB_C
        //public static readonly int DB_NoMatchDBType = 110001;

        //public static readonly int DB_NoDataSelected = 110002;
        //public static readonly int DB_NoDataUpdate = 110003;
        //public static readonly int DB_ConnStringIsSpace = 110004;

        //public static readonly int DB_LogWrited = 100016;
        //public static readonly int DB_SelectFail = 100017;
        //public static readonly int DB_UpdateFail = 100018;
        //public static readonly int DB_InsertFail = 100019;

        public static readonly int CMD_UpdateCmdStsErr = 101001;
        public static readonly int CMD_InsertErr_CmdSno = 101002;
        public static readonly int CMD_InsertErr_CmdMode = 101003;
        public static readonly int CMD_InsertErr_Source = 101004;
        public static readonly int CMD_InsertErr_Destination = 101005;
        public static readonly int CMD_InsertErr_Priority = 101006;
        public static readonly int CMD_InsertErr_LocSize = 101007;
        public static readonly int CMD_InsertErr_CarNo = 101008;
        public static readonly int CMD_SourceError = 101009;
        public static readonly int CMD_CmdModeError = 101010;
        public static readonly int CMD_DesLocError = 101011;
        public static readonly int CMD_UpdateTraceErr = 101012;
        public static readonly int CMD_InsertErr_EquNoErr = 101013;
        public static readonly int CMD_GetNewLocErr = 101014;
        public static readonly int CMD_GetEPLocErr = 101015;
        public static readonly int CMD_UpdateLocStsErr = 101016;
        public static readonly int CMD_UpdateTransferStsErr = 101017;

        public static readonly int PLC_ReadDeviceErr = 102001;
        public static readonly int PLC_WriteDeviceErr = 102002;
        public static readonly int PLC_SetDeviceBitFormatErr = 102003;
        public static readonly int PLC_NotConnect = 102004;
        public static readonly int PLC_BufferDataErr = 102005;
        public static readonly int PLC_NoDataToWrite = 102006;
        public static readonly int PLC_AddressIsSpace = 102007;
        public static readonly int PLC_DataNegative = 102008;
        public static readonly int PLC_HSFail = 102009;
        public static readonly int PLC_WritePLCErr = 102010;
        public static readonly int PLC_SetDeviceBitErr = 102011;

        public static readonly int Proc_NeedR2R = 103001;
        public static readonly int Proc_NotNeedR2R = 103002;
        public static readonly int Proc_GetInSideLocStsErr = 103003;
        public static readonly int Proc_InSideLocBooked = 103004;
        public static readonly int Proc_NeedBookingInSideLoc = 103005;
        public static readonly int Proc_SelectCaseDataErr = 103006;
        public static readonly int Proc_FindDesLocFail = 103007;
        public static readonly int Proc_InSideLocStsNoReady = 103008;
        public static readonly int Proc_InSideLocSchedulingButNoPallet = 103009;
        public static readonly int Proc_InSideLocStsErr = 103010;               //未知的儲位狀態
        public static readonly int Proc_InSideLocNoCmdBooking = 103011;         //已預約儲位 但無相關命令
        public static readonly int Proc_InSideLocIsDoubleStorage = 103012;
        public static readonly int Proc_InSideLocIsPSts = 103013;
        public static readonly int Proc_NeedR2RforPSts = 103014;
        public static readonly int proc_NextStn = 103015;
        public static readonly int proc_NextCmd = 103016;

        public static readonly int SM_WriteCMD2SMErr = 104001;

        public static readonly int CMD_UpdateCSTStsErr = 105001;
        public static readonly int CMD_UpdateCSTPosErr = 105002;
        public static readonly int CMD_UpdateCSTStsPosErr = 105003;
    }
}