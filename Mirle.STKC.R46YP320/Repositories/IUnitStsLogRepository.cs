﻿using Mirle.STKC.R46YP320.Model;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface IUnitStsLogRepository
    {
        void Insert(UnitStsLogDTO dto);

        void UpdateEndDT(UnitStsLogDTO dto);

        void DeleteEndDTIsNull();
    }
}