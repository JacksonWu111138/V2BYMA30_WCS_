using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class PortDefRepository : IPortDefRepository
    {
        private readonly MSSqlDbService _dbService;
        private readonly LCSInfo _lcsInfo;

        public PortDefRepository(MSSqlDbService dbService, LCSInfo lcsInfo)
        {
            _dbService = dbService;
            _lcsInfo = lcsInfo;
        }

        public IEnumerable<PortDefDTO> GetAll()
        {
            var sql = $"SELECT * FROM PortDef WHERE DeviceID = @DeviceID";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<PortDefDTO>(sql, new { DeviceID = _lcsInfo.Stocker.StockerId });
            }
        }
    }
}