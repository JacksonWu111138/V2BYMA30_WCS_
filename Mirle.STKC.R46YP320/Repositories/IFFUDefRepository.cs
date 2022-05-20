﻿using Mirle.STKC.R46YP320.Model;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Repositories
{
    public interface IFFUDefRepository
    {
        IEnumerable<FFUDefDTO> GetAll();
    }
}