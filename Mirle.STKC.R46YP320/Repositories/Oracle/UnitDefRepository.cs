﻿using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class UnitDefRepository : IUnitDefRepository
    {
        private readonly OracleDbService _dbService;

        public UnitDefRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<UnitDefDTO> GetAll(string stockerId)
        {
            var sql = "select * from UnitDef where StockerID=:StockerId";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<UnitDefDTO>(sql, new { StockerId = stockerId });
            }
        }
    }
}