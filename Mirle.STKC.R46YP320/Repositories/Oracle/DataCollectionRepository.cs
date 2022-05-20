using System;
using Dapper;
using Mirle.STKC.R46YP320.Service;
using Mirle.STKC.R46YP320.Model;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class DataCollectionRepository : IDataCollectionRepository
    {
        private readonly OracleDbService _dbService;

        public DataCollectionRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public void Insert(DataCollectionDTO dto)
        {
            var sql = @"INSERT INTO DATACOLLECTION (TIME, NAME, VALUE) 
VALUES (:TIME, :NAME, :VALUE)";

            using (var con = _dbService.GetDbConnection())
            {
                var affectedRows = con.Execute(sql, dto);
            }
        }

        public int DeleteByTimeBefore(DateTime now)
        {
            var sql = @"DELETE FROM DATACOLLECTION WHERE TIME < :TIME";
            using (var con = _dbService.GetDbConnection())
            {
                var affectedRows = con.Execute(sql, new { TIME = now });
                return affectedRows;
            }
        }
    }
}
