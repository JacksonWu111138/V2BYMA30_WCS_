using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class MileageLogRepository : IMileageLogRepository
    {
        private readonly MSSqlDbService _dbService;

        public MileageLogRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public void Insert(IEnumerable<MileageLogDTO> dtos)
        {
            var updateSql =
                "update MileageLog set DataValue=@DataValue, LogDT=@LogDT where StockerId=@StockerId and UnitId=@UnitId and MileageType=@MileageType and KeyDate=@KeyDate";
            var insertSql =
                "insert into MileageLog (StockerID, UnitID, MileageType, DataValue, LogDT, KeyDate)values(@StockerId, @UnitId, @MileageType, @DataValue, @LogDT, @KeyDate)";

            using (var con = _dbService.GetDbConnection())
            {
                foreach (var dto in dtos)
                {
                    var affectedRows = con.Execute(updateSql, dto);
                    if (affectedRows == 0)
                    {
                        con.Execute(insertSql, dto);
                    }
                }
            }
        }
    }
}