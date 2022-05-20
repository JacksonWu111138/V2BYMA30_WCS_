using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class OMScheduleRepository : IOMScheduleRepository
    {
        private readonly OracleDbService _dbService;

        public OMScheduleRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<OMScheduleDTO> GetCurrentDisplay()
        {
            var sql = @"SELECT * FROM OMSchedule
WHERE systimestamp > to_date(STRDT, 'yyyy-mm-dd hh24:mi:ss') and systimestamp<to_date(ENDDT,'yyyy-mm-dd hh24:mi:ss')
AND IsDisplay = 'Y'";

            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<OMScheduleDTO>(sql);
            }
        }
    }
}