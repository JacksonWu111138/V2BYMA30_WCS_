using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Linq;

namespace Mirle.STKC.R46YP320.Repositories.MSSQL
{
    public class SnoctrlRepository : ISnoctrlRepository
    {
        private readonly MSSqlDbService _dbService;

        public SnoctrlRepository(MSSqlDbService dbService)
        {
            _dbService = dbService;
        }

        public SnoctrlDTO GetBySnotype(string snotype)
        {
            using (var con = _dbService.GetDbConnection())
            {
                var selectSql = "Select * from SNO_CTL where SnoTyp=@snotype";
                return con.Query<SnoctrlDTO>(selectSql, new { snotype = snotype })
                    .FirstOrDefault();
            }
        }

        public void Update(SnoctrlDTO dto)
        {
            using (var con = _dbService.GetDbConnection())
            {
                var updateSql = "Update SNO_CTL set SNO=@Sno, TrnDate=@TrnDT where SnoTyp=@SnoType";
                con.Execute(updateSql, dto);
            }
        }

        public void Insert(SnoctrlDTO dto)
        {
            using (var con = _dbService.GetDbConnection())
            {
                var insertSql = "INSERT INTO SNO_CTL(TrnDate,SnoTyp,SNO) VALUES (@TrnDT, @SnoType, @Sno)";
                con.Execute(insertSql, dto);
            }
        }
    }
}