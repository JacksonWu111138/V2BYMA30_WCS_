using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Model.Define;
using Mirle.STKC.R46YP320.Service;
using System;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class FFUAlarmDataRepository : IFFUAlarmDataRepository
    {
        private readonly MSSqlDbService _dbService;

        public FFUAlarmDataRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public void Insert(FFUAlarmDataDTO dto)
        {
            dto.StrDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            dto.AlarmSts = AlarmStates.Set;
            dto.ReportFlag = ReportFlag.NotyetReport;
            var sql = @"INSERT INTO FFUAlarmData (EQID, AlarmSts, AlarmCode, STRDT, ReportFlag, AlarmLoc)
values (@EQId, @AlarmSts, @AlarmCode, @StrDT, @ReportFlag, @AlarmLoc)";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdateClearedAlarmByEqIdAndAlarmCode(string eqId, string alarmCode)
        {
            var sql = @"UPDATE FFUAlarmData SET AlarmSts=@NewAlarmSts, ENDDT=@EndDT, ReportFlag=@ReportFlag
WHERE EQID=@EqId and AlarmCode=@AlarmCode and AlarmSts=@OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                ReportFlag = ReportFlag.NotyetReport,
                EqId = eqId,
                AlarmCode = alarmCode,
                OldAlarmSts = AlarmStates.Set,
            };
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdateAllClearedAlarm()
        {
            var sql = @"UPDATE FFUAlarmData SET AlarmSts=@NewAlarmSts, ENDDT=@EndDT, ReportFlag=@ReportFlag WHERE AlarmSts=@OldAlarmSts";

            var dto = new
            {
                NewAlarmSts = AlarmStates.Cleared,
                EndDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                ReportFlag = ReportFlag.NotyetReport,
                OldAlarmSts = AlarmStates.Set,
            };
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }
    }
}