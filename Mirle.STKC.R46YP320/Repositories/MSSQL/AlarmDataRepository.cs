using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Model.Define;
using Mirle.STKC.R46YP320.Service;
using System;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class AlarmDataRepository : IAlarmDataRepository
    {
        private readonly MSSqlDbService _dbService;

        public AlarmDataRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public void Insert(AlarmDataDTO dto)
        {
            var sql = @"insert into AlarmData (DeviceID,UnitID,AlarmCode,StartDT,AlarmLoc,CommandID,CommandID_RF,CSTID,CSTLoc,Source,Destination,MPLCAlarmIndex,AlarmType)
values(@StockerId, @EqId, @AlarmCode, @StrDT, @AlarmLoc, @CommandId, @CommandId_RF, @CstId, @CstLoc, @Source, @Dest, @MPLCAlarmIndex, @AlarmType)";
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
            var sql = @"insert into AlarmData(DeviceID, UnitID, AlarmState, AlarmCode, StartDT, ENDDT, RECOVERTIME, AlarmTime, REPORTFLAG, AlarmLoc, CommandID, CommandID_RF, CSTID, CSTLoc, Source, Destination, MPLCAlarmIndex, AlarmType)
values(@StockerId, @EQId, @AlarmSts, @AlarmCode, @StrDT, @EndDT, @RecoverTime, @AlarmTime, @ReportFlag, @AlarmLoc, @CommandId, @CommandId_RF, @CstId, @CstLoc, @Source, @Dest, @MPLCAlarmIndex, @AlarmType)";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdateClearedAlarmByEqIdAndAlarmCode(string stockerId, string eqId, string alarmCode)
        {
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var sql = $@"update AlarmData set AlarmState=@NewAlarmSts, ENDDT=@EndDT,
RecoverTime=datediff(SECOND,IIF(SafeDoorOpenDT='', IIF(AlarmResetDT='',StartDT, AlarmResetDT),SafeDoorOpenDT), '{now}'),
AlarmTime=datediff(SECOND,StartDT, '{now}'),
ReportFlag=@ReportFlag
where DeviceID=@StockerId
and UnitID=@EqId
and AlarmCode=@AlarmCode
and AlarmState=@OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = now,
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
and AlarmData.DeviceID=@StockerId
and AlarmData.AlarmState =@CurrentAlarmSts";
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
and AlarmData.DeviceID=@StockerId
and AlarmData.UnitID=@EqId
and AlarmData.AlarmState =@CurrentAlarmSts";
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
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var sql = $@"update AlarmData set AlarmState=@NewAlarmSts, ENDDT=@EndDT,
RecoverTime=datediff(SECOND,IIF(SafeDoorOpenDT='', IIF(AlarmResetDT='',StartDT, AlarmResetDT),SafeDoorOpenDT), '{now}'),
AlarmTime=datediff(SECOND,StartDT, '{now}'),
ReportFlag=@ReportFlag
where DeviceID=@StockerId and AlarmState=@OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = now,
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
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var sql = $@"update AlarmData set AlarmState=@NewAlarmSts, ENDDT=@EndDT,
RecoverTime=datediff(SECOND,IIF(SafeDoorOpenDT='', IIF(AlarmResetDT='',StartDT, AlarmResetDT),SafeDoorOpenDT), '{now}'),
AlarmTime=datediff(SECOND,StartDT, '{now}'),
ReportFlag=@ReportFlag
where DeviceID=@StockerId and UnitID=@EqId and AlarmState=@OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = now,
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
            var sql = $"update AlarmData set SafeDoorClosedDT=@PLCDoorClosedDT where DeviceID=@StockerId and AlarmType=@AlarmType and AlarmState=@AlarmSts and SafeDoorClosedDT = ''";
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
            var sql = $"update AlarmData set SafeDoorOpenDT=@PLCDoorOpenDT where DeviceID=@StockerId and AlarmType=@AlarmType and AlarmState=@AlarmSts and SafeDoorOpenDT = ''";
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
            var sql = "update AlarmData set AlarmResetDT=@PLCResetAlarmDT where DeviceID=@StockerId and UnitID=@EQId and AlarmState=@AlarmSts and AlarmResetDT = ''";
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
            var sql = "update AlarmData set AlarmResetDT=@PLCResetAlarmDT where DeviceID=@StockerId and UnitID=@EQId and AlarmCode=@AlarmCode and AlarmState=@AlarmSts and AlarmResetDT =''";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }
    }
}