using Mirle.STKC.R46YP320.Model;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface IMileageLogRepository
    {
        void Insert(IEnumerable<MileageLogDTO> dtos);
    }
}