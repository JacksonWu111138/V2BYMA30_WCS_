using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class ShelfDefRepository : IShelfDefRepository
    {
        private readonly MSSqlDbService _dbService;

        public ShelfDefRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<ShelfDefDTO> GetAll()
        {
            var sql = "SELECT * FROM ShelfDef";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<ShelfDefDTO>(sql);
            }
        }
    }
}