﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Strategies
{
    interface ICalculateGift
    {
        public decimal Calculate(decimal money);
    }
}
