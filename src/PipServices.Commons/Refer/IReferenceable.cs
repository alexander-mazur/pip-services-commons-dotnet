﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public interface IReferenceable
    {
        void SetReferences(IReferences references);
    }
}
