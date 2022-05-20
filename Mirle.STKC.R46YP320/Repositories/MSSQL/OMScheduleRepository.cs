using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class OMScheduleRepository : IOMScheduleRepository
    {
        private readonly MSSqlDbService _dbService;

        public OMScheduleRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<OMScheduleDTO> GetCurrentDisplay()
        {
            var sql = $@"SELECT * FROM OMSchedule
WHERE STRDT < '{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}'
AND ENDDT > '{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}'
AND IsDisplay = 'Y'";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<OMScheduleDTO>(sql);
            }
        }
    }
}