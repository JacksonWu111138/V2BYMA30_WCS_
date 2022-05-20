using System;
using Mirle.STKC.R46YP320.Model;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface IDataCollectionRepository
    {
        void Insert(DataCollectionDTO dto);
        int DeleteByTimeBefore(DateTime now);
    }
}
