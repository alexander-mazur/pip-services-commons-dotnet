﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Counters
{
    public enum CounterType : int
    {
        Interval = 0,
        LastValue = 1,
        Statistics = 2,
        Timestamp = 3,
        Increment = 4
    }
}