using Dapper;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories.Oracle
{
    public class UserMstRepository : IUserMstRepository
    {
        private readonly OracleDbService _dbService;

        public UserMstRepository(OracleDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<UserMstDTO> GetByUserId(string userId)
        {
            var sql = $"SELECT PASSWORD FROM USERMST WHERE USERID='{userId}'";
            using (var con = _dbService.GetDbConnection())
            {
                return con.Query<UserMstDTO>(sql);
            }
        }

        public void UpdatePasswordByUserId(string userId, string password)
        {
            var sql = $"Update USERMST SET PASSWORD='{password}' WHERE USERID='{userId}'";
            using (var con = _dbService.GetDbConnection())
            {
                con.Execute(sql);
            }
        }
    }
}