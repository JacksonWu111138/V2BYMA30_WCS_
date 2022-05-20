using Mirle.STKC.R46YP320.Model;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface IUserMstRepository
    {
        IEnumerable<UserMstDTO> GetByUserId(string userId);

        void UpdatePasswordByUserId(string userId, string password);
    }
}