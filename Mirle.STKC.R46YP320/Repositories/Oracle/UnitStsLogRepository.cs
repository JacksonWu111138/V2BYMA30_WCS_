using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class UnitStsLogRepository : IUnitStsLogRepository
    {
        private readonly OracleDbService _dbService;

        public UnitStsLogRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public void Insert(UnitStsLogDTO dto)
        {
            var sql = "insert into UnitStsLog(StockerID,UnitID,STRDT,Status) values (:StockerId, :UnitId, :StrDT, :Status)";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql, dto);
            }
        }

        public void UpdateEndDT(UnitStsLogDTO dto)
        {
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var update = $@"UPDATE UnitStsLog SET ENDDT='{now}', TotalSecs=ROUND((SYSDATE-TO_DATE(SUBSTR(STRDT,1,19),'YYYY-MM-DD HH24:MI:SS'))*24*60*60,0)
WHERE StockerID=:StockerId AND UnitID=:UnitId AND ENDDT is null";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(update, dto);
            }
        }

        public void DeleteEndDTIsNull()
        {
            var sql = "DELETE FROM UnitStsLog WHERE EndDT IS NULL";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql);
            }
        }
    }
}