using System;

namespace PipServices.Commons.Count
{
    /// <summary>
    /// Provides callback to end measuring execution time interface and update interval counter.
    /// </summary>
    public class Timing : IDisposable
    {
        private readonly int _start;
        private readonly ITimingCallback _callback;
        private readonly string _counter;

        /// <summary>
        /// Creates instance of timing object that doesn't record anything
        /// </summary>
        public Timing() { }

        /// <summary>
        /// Creates instance of timing object that calculates elapsed time
        /// and stores it to specified performance counters component under specified name.
        /// </summary>
        /// <param name="counter">a name of the counter to record elapsed time interval.</param>
        /// <param name="callback">a performance counters component to store calculated value.</param>
        public Timing(string counter, ITimingCallback callback)
        {
            _counter = counter;
            _callback = callback;
            _start = Environment.TickCount;
        }

        /// <summary>
        /// Completes measuring time interval and updates counter.
        /// </summary>
        public void EndTiming()
        {
            if (_callback == null)
                return;

            double elapsed = Environment.TickCount - _start;

            _callback.EndTiming(_counter, elapsed);
        }

        public void Dispose()
        {
            EndTiming();
        }
    }
}
