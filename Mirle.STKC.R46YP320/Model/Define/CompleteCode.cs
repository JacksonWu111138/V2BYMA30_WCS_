namespace Mirle.STKC.R46YP320.Model.Define
{
    public static class CompleteCode
    {
        public const string PickProc_T2TimeOut = "C0";             //取物 T2 time out
        public const string PickProc_EQLReqOn = "C1";	            //取物檢測到EQ L-REQ訊號ON
        public const string PickProc_EQUReqOff = "C2";             //取物檢測到EQ U-REQ訊號OFF
        public const string PickProc_EQReadyOn = "C3";	            //取物檢測到EQ Ready訊號ON
        public const string PickProc_EQOnlineOffBeforePick = "C4"; //RM取物前行程EQ Online信號中斷
        public const string PickProc_EQRYOff = "C5";	            //RM取物Tr-on檢測到EQ RY信號中斷
        public const string PickProc_EQNoCST = "C6";               //EQ Port站口無物
        public const string PickProc_EQNotForkAccess = "C7";       //EQ Port不允許Fork存取
        public const string PickProc_T5TimeOut = "C8";             //取物 T5 time out
        public const string PickProc_EQUReqOnAfterRMFinish = "C9";	//RM取物完檢測到EQ U-REQ訊號ON
        public const string PickProc_EQOnLineOffAfterRMFinish = "CA";	//RM取物完檢測到EQ Online OFF
        public const string DeposProc_T2TimeOut = "D0";	        //置物 T2 time out
        public const string DeposProc_EQLReqOff = "D1";	        //置物檢測到EQ L-REQ訊號OFF
        public const string DeposProc_EQUReqOn = "D2";	            //置物檢測到EQ U-REQ訊號ON
        public const string DeposProc_EQReadyOn = "D3";	        //置物檢測到EQ Ready訊號ON
        public const string DeposProc_EQOnlineOffBeforeDepos = "D4";	//RM置物前行程EQ Online信號中斷
        public const string DeposProc_EQRYOffBeforeDepos = "D5";	//RM置物Tr-on檢測到EQ RY信號中斷
        public const string DeposProc_EQHaveCST = "D6";	        //EQ Port站口有物
        public const string DeposProc_EQNotForkAccess = "D7";	    //EQ Port不允許Fork存置
        public const string DeposProc_T5TimeOut = "D8";	        //置物 T5 time out
        public const string DeposProc_EQLReqOnAfterDepos = "D9";	//RM置物完檢測到EQ L-REQ訊號ON
        public const string DeposProc_EQOnlineOffAfterDepos = "DA";	//RM置物完檢測到EQ Online OFF
        public const string InlineInterlockError_OnLine = "E0";	//Inline Interlock Error(On-Line)
        public const string TransferRequestWrong = "E1";	        //Transfer Request Wrong.
        public const string EmptyRetrieval = "E2";	                //儲位空出庫
        public const string ScanIDReadError = "E3";	                //Scan ID Read Error
        public const string IDMismatch = "E4";	                    //ID Mismatch
        public const string ScanNoResponse = "E5";	                //Scan No Response
        public const string NoCST = "E6";	                        //檢知無CST
        public const string IDReadError = "E7";	                    //ID Read Error
        public const string NoResponse = "E8";	                    //No Response
        public const string FromCommandAbout = "E9";	            //From Command about
        public const string MoveScanCommandAbout = "EA";	        //Move/Scan command about
        public const string CassetteTypeMissmach = "EB";	        //Cassette Type MissMach
        public const string DoubleStorage = "EC";	                //Double storage
        public const string InlineInterlockError_LD = "ED";	    //Inline Interlock Error(LD)
        public const string InlineInterlockError_ULD = "EE";	    //Inline Interlock Error(ULD)
        public const string HMIUserForceAbortCommand = "EF";	    //HMI User Force Abort Command
        public const string HMIUserForceFinishCommand = "FF";	    //HMI User Force Finish Command
        public const string Success_FromReturnCodeAck = "91";	    //From Return code Ack
        public const string Success_ToReturnCode = "92";	        //To Return code
        public const string Success_CraneIsRunningRetryMoving = "94";	//Crane is running retry moving
        public const string Success_ScanComplete = "97";	        //Scan complete
        public const string Success_IdleTimeOutReset = "99";	        //Idle Timeout Reset Abnormal complete
        public const string Success_AbortDuringCycle1 = "93";	                //AbortCMD in Cycle1

        public const string DeposProc_Obstruction = "DB";          //RM置物前發生 Obstruction
        public const string PickProc_Obstruction = "CB";	        //RM取物前發生 Obstruction

        public const string PickupCycle_Error = "F1";
        public const string DepositCycle_Error = "F2";

        // Form STKC
        //retry
        public const string CannotRetrieveFromSourcePortFromSTKC_P0 = "P0";    //STKC 來源Port無法取物

        public const string CannotDepositToDestinationPortFromSTKC_P1 = "P1";  //STKC 目的Port無法置物

        //abort
        public const string CannotRetrieveHasCarrierOnCraneFromSTKC_P2 = "P2"; //STKC 車上有物無法取物

        public const string CannotDepositNoCarrierOnCraneFromSTKC_P3 = "P3";   //STKC 車上無物無法置物

        //complete
        public const string CannotScanHasCarrierOnCraneFromSTKC_P4 = "P4";     //STKC 車上有物無法Scan

        public const string CannotExcuteFromSTKC = "PD";            //STKC 判斷地上盤無法執行該命令
        public const string CommandTimeoutFromSTKC = "PE";          //STKC 下命令給地上盤命令後，地上盤未有反應或執行超過10分鐘
    }
}