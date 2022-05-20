using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Linq;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class SnoctrlRepository : ISnoctrlRepository
    {
        private readonly OracleDbService _dbService;

        public SnoctrlRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public SnoctrlDTO GetBySnotype(string snotype)
        {
            using (var con = _dbService.GetDbConnection())
            {
                var selectSql = "Select * from SnoCtrl where SNOTYPE=:snotype";
                return con.Query<SnoctrlDTO>(selectSql, new { snotype = snotype })
                    .FirstOrDefault();
            }
        }

        public void Update(SnoctrlDTO dto)
        {
            using (var con = _dbService.GetDbConnection())
            {
                var updateSql = "Update SnoCtrl set SNO=:Sno, TrnDT=:TrnDT where SNOTYPE=:SnoType";
                con.Execute(updateSql, dto);
            }
        }

        public void Insert(SnoctrlDTO dto)
        {
            using (var con = _dbService.GetDbConnection())
            {
                var insertSql = "INSERT INTO SnoCtrl(TrnDT,SNOTYPE,SNO) VALUES (:TrnDT, :SnoType, :Sno)";
                con.Execute(insertSql, dto);
            }
        }
    }
}