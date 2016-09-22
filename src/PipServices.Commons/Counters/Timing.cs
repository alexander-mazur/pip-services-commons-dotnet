using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Counters
{
    public class Timing
    {
        private long _start;
        private string _counter;
        private ITimingCallback _cb;

        public Timing()
        { }

        public Timing(string name, ITimingCallback cb)
        {
            _counter = name;
            _cb = cb;
            _start = DateTime.Now.Ticks;
        }

        public void EndTiming()
        {
            if (_cb != null)
            {
                _cb.EndTiming(_counter, DateTime.Now.Ticks - _start);
            }
        }
    }
}