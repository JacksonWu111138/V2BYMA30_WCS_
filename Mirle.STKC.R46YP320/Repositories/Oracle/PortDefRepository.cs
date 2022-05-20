using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class PortDefRepository : IPortDefRepository
    {
        private readonly OracleDbService _dbService;

        public PortDefRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<PortDefDTO> GetAll()
        {
            var sql = "SELECT * FROM PortDef";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<PortDefDTO>(sql);
            }
        }
    }
}