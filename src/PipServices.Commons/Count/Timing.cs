using System;

namespace PipServices.Commons.Count
{
    /**
     * Provides callback to end measuring execution time interface and update interval counter.
     */
    public class Timing
    {
        private readonly int _start;
        private readonly ITimingCallback _callback;
        private readonly string _counter;

        /**
         * Creates instance of timing object that doesn't record anything
         */
        public Timing() { }

        /**
         * Creates instance of timing object that calculates elapsed time
         * and stores it to specified performance counters component under specified name.
         * @param counter a name of the counter to record elapsed time interval.
         * @param callback a performance counters component to store calculated value.
         */
        public Timing(string counter, ITimingCallback callback)
        {
            _counter = counter;
            _callback = callback;
            _start = Environment.TickCount;
        }

        /**
         * Completes measuring time interval and updates counter.
         */
        public void EndTiming()
        {
            if (_callback == null)
                return;

            double elapsed = Environment.TickCount - _start;

            _callback.EndTiming(_counter, elapsed);
        }
    }
}
