﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public interface IRepertoirManager
    {
        Task<DayRepertoir> GetDayRepertoir(DateTime date);
    }
}
