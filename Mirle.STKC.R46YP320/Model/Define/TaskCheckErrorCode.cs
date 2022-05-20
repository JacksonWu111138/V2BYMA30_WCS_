namespace Mirle.STKC.R46YP320.Model.Define
{
    public static class TaskCheckErrorCode
    {
        public static readonly int GetLocateCraneNoFail = 80001;
        public static readonly int stuTransferCMDDataLoss = 80002;
        public static readonly int CreateTransferCMDFail = 80003;
        public static readonly int CreateTransferCMDFail_BookingSource = 80004;
        public static readonly int CreateTransferCMDFail_BookingDestination = 80005;
        public static readonly int CreateTransferCMDFail_GetOHCVPort = 80006;
        public static readonly int CreateTransferCMDFail_GetTransferNo = 80007;
        public static readonly int CreateTransferCMDFail_GetHandoffShelfID = 80008;
        public static readonly int S2F41RCMDError = 80009;
        public static readonly int ChkPortSts_PortTypeErr = 80010;
        public static readonly int ChkPortSts_PortNoOutOfRange = 80011;
        public static readonly int ChkPortSts_NotInService = 80012;
        public static readonly int ChkPortSts_AlarmOn = 80013;
        public static readonly int ChkPortSts_NoUDRQ = 80014;
        public static readonly int ChkPortSts_NoLDRQ = 80015;
        public static readonly int ChkPortSts_ForkAccessOff = 80016;
        public static readonly int ChkPortSts_PortHaveCST = 80017;
        public static readonly int ChkPortSts_PortNoCST = 80018;
        public static readonly int ChkPortSts_DirModeErr_InModeOn = 80019;
        public static readonly int ChkPortSts_DirModeErr_InModeOff = 80020;
        public static readonly int ChkPortSts_DirModeErr_OutModeOn = 80021;
        public static readonly int ChkPortSts_DirModeErr_OutModeOff = 80022;
        public static readonly int CreateTransferCMDFail_ZoneCapactityNotEnough = 80023;
    }
}