using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Model.Define;
using Mirle.STKC.R46YP320.Service;
using System;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class AlarmDataRepository : IAlarmDataRepository
    {
        private readonly OracleDbService _dbService;

        public AlarmDataRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public void Insert(AlarmDataDTO dto)
        {
            var sql = @"insert into AlarmData (StockerID,EQID,AlarmCode,STRDT,AlarmLoc,CommandID,CommandID_RF,CSTID,CSTLoc,Source,Dest,StockerCraneID,MPLCAlarmIndex,AlarmType,SRSEQ)
values(:StockerId, :EqId, :AlarmCode, :StrDT, :AlarmLoc, :CommandId, :CommandId_RF, :CstId, :CstLoc, :Source, :Dest, :StockerCraneId, :MPLCAlarmIndex, :AlarmType, :SRSEQ)";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void InsertWarning(AlarmDataDTO dto)
        {
            dto.AlarmSts = AlarmStates.SetCleared;
            dto.RecoverTime = 1;
            dto.AlarmTime = 1;
            dto.ReportFlag = ReportFlag.NotyetReport;
            var sql = @"insert into AlarmData(StockerID, EQID, ALARMSTS, AlarmCode, STRDT, ENDDT, RECOVERTIME, AlarmTime, REPORTFLAG, AlarmLoc, CommandID, CommandID_RF, CSTID, CSTLoc, Source, Dest, StockerCraneID, MPLCAlarmIndex, AlarmType)
values(:StockerId, :EQId, :AlarmSts, :AlarmCode, :StrDT, :EndDT, :RecoverTime, :AlarmTime, :ReportFlag, :AlarmLoc, :CommandId, :CommandId_RF, :CstId, :CstLoc, :Source, :Dest, :StockerCraneId, :MPLCAlarmIndex, :AlarmType)";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdateClearedAlarmByEqIdAndAlarmCode(string stockerId, string eqId, string alarmCode)
        {
            var sql = @"update AlarmData set AlarmSts=:NewAlarmSts, ENDDT=:EndDT,
RecoverTime=nvl(ROUND((SYSDATE-TO_DATE(SUBSTR(PLCDOOROPENDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0), ROUND((SYSDATE-TO_DATE(SUBSTR(PLCRESETALARMDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0)),
AlarmTime=ROUND((SYSDATE-TO_DATE(SUBSTR(STRDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0),
ReportFlag=:ReportFlag
where StockerID=:StockerId
and EQID=:EqId
and AlarmCode=:AlarmCode
and AlarmSts=:OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                ReportFlag = ReportFlag.NotyetReport,
                StockerId = stockerId,
                EqId = eqId,
                AlarmCode = alarmCode,
                OldAlarmSts = AlarmStates.Set,
            };
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public IEnumerable<AlarmDataDTO> GetAllCurrentAlarm(string stockerId)
        {
            var sql = @"select * from AlarmData,AlarmDef
where AlarmData.AlarmCode=AlarmDef.AlarmCode
and AlarmData.StockerID=:StockerId
and AlarmData.AlarmSts =:CurrentAlarmSts";
            var dto = new
            {
                StockerId = stockerId,
                CurrentAlarmSts = AlarmStates.Set,
            };
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<AlarmDataDTO>(sql, dto);
            }
        }

        public IEnumerable<AlarmDataDTO> GetCurrentAlarmByEqId(string stockerId, string eqId)
        {
            var sql = @"select * from AlarmData,AlarmDef
where AlarmData.AlarmCode=AlarmDef.AlarmCode
and AlarmData.StockerID=:StockerId
and AlarmData.EQID=:EqId
and AlarmData.AlarmSts =:CurrentAlarmSts";
            var dto = new
            {
                StockerId = stockerId,
                EqId = eqId,
                CurrentAlarmSts = AlarmStates.Set,
            };
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<AlarmDataDTO>(sql, dto);
            }
        }

        public void UpdateClearedAlarm(string stockerId)
        {
            var sql = @"update AlarmData set AlarmSts=:NewAlarmSts, ENDDT=:EndDT,
RecoverTime=nvl(ROUND((SYSDATE-TO_DATE(SUBSTR(PLCDOOROPENDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0), ROUND((SYSDATE-TO_DATE(SUBSTR(PLCRESETALARMDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0)),
AlarmTime=ROUND((SYSDATE-TO_DATE(SUBSTR(STRDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0),
ReportFlag=:ReportFlag
where StockerID=:StockerId and AlarmSts=:OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                ReportFlag = ReportFlag.NotyetReport,
                StockerId = stockerId,
                OldAlarmSts = AlarmStates.Set,
            };
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdateClearedAlarmByEqId(string stockerId, string eqId)
        {
            var sql = @"update AlarmData set AlarmSts=:NewAlarmSts, ENDDT=:EndDT,
RecoverTime=nvl(ROUND((SYSDATE-TO_DATE(SUBSTR(PLCDOOROPENDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0), ROUND((SYSDATE-TO_DATE(SUBSTR(PLCRESETALARMDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0)),
AlarmTime=ROUND((SYSDATE-TO_DATE(SUBSTR(STRDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0),
ReportFlag=:ReportFlag
where StockerID=:StockerId and EQID=:EqId and AlarmSts=:OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                ReportFlag = ReportFlag.NotyetReport,
                StockerId = stockerId,
                EqId = eqId,
                OldAlarmSts = AlarmStates.Set,
            };
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdatePLCDoorClosedDT(string stockerId)
        {
            var dto = new AlarmDataDTO()
            {
                StockerId = stockerId,
                AlarmType = AlarmTypes.Stocker,
                AlarmSts = AlarmStates.Set,
                PLCDoorClosedDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            };
            var sql = $"update AlarmData set PLCDOORCLOSEDDT=:PLCDoorClosedDT where StockerID=:StockerId and AlarmType=:AlarmType and ALARMSTS=:AlarmSts and PLCDOORCLOSEDDT IS NULL";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdatePLCDoorOpenDT(string stockerId)
        {
            var dto = new AlarmDataDTO()
            {
                StockerId = stockerId,
                AlarmType = AlarmTypes.Stocker,
                AlarmSts = AlarmStates.Set,
                PLCDoorOpenDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            };
            var sql = $"update AlarmData set PLCDOOROPENDT=:PLCDoorOpenDT where StockerID=:StockerId and AlarmType=:AlarmType and ALARMSTS=:AlarmSts and PLCDOOROPENDT IS NULL";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdatePLCResetAlarmDT(string stockerId, string eqId)
        {
            var dto = new AlarmDataDTO()
            {
                StockerId = stockerId,
                EQId = eqId,
                AlarmSts = AlarmStates.Set,
                PLCResetAlarmDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            };
            var sql = "update AlarmData set PLCRESETALARMDT=:PLCResetAlarmDT where StockerID=:StockerId and EQID=:EQId and ALARMSTS=:AlarmSts and PLCRESETALARMDT IS NULL";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdatePLCResetAlarmDT(string stockerId, string eqId, string alarmCode)
        {
            var dto = new AlarmDataDTO()
            {
                StockerId = stockerId,
                EQId = eqId,
                AlarmCode = alarmCode,
                AlarmSts = AlarmStates.Set,
                PLCResetAlarmDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            };
            var sql = "update AlarmData set PLCRESETALARMDT=:PLCResetAlarmDT where StockerID=:StockerId and EQID=:EQId and AlarmCode=:AlarmCode and ALARMSTS=:AlarmSts and PLCRESETALARMDT IS NULL";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }
    }
}