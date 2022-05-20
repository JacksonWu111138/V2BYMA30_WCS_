using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class FFUDefRepository : IFFUDefRepository
    {
        private readonly MSSqlDbService _dbService;

        public FFUDefRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<FFUDefDTO> GetAll()
        {
            var sql = "SELECT * FROM FFUDef";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<FFUDefDTO>(sql);
            }
        }
    }
}