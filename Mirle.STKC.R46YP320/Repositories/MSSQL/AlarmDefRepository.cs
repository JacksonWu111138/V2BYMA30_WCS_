using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class AlarmDefRepository : IAlarmDefRepository
    {
        private readonly MSSqlDbService _dbService;

        public AlarmDefRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<AlarmDefDTO> GetAll()
        {
            var sql = "SELECT * FROM AlarmDef";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<AlarmDefDTO>(sql);
            }
        }
    }
}