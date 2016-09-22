using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Counters
{
    public interface ITimingCallback
    {
        void EndTiming(string name, float elapsed);
    }
}
