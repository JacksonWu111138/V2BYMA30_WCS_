namespace Mirle.STKC.R46YP320.Model
{
    public class AlarmDataDTO
    {
        //AlarmData
        public string StockerId { get; set; }

        public AlarmTypes AlarmType { get; set; }
        public string EQId { get; set; }
        public AlarmStates AlarmSts { get; set; }
        public string AlarmCode { get; set; }
        public string StrDT { get; set; }
        public string EndDT { get; set; }
        public int RecoverTime { get; set; }
        public int AlarmTime { get; set; }
        public string ReportFlag { get; set; }
        public string AlarmLoc { get; set; }
        public string CommandId { get; set; }
        public string CommandId_RF { get; set; }
        public string CstId { get; set; }
        public string CstLoc { get; set; }
        public string Source { get; set; }
        public string Dest { get; set; }
        public string StockerUnitStatus { get; set; }
        public string StockerCraneId { get; set; }
        public string MPLCAlarmIndex { get; set; }
        public string PLCDoorOpenDT { get; set; }
        public string PLCDoorClosedDT { get; set; }
        public string PLCResetAlarmDT { get; set; }
        public string SRSEQ { get; set; }

        //AlarmDef
        public string AlarmDesc { get; set; }
    }
}