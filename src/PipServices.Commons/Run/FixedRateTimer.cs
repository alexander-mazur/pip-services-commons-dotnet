using System.Threading;

namespace PipServices.Commons.Run
{
    public class FixedRateTimer : IClosable
    {
        public INotifiable Task { get; set; }
        public int Delay { get; set; }
        public int Interval { get; set; }
        private Timer _timer;
        public bool Started { get; private set; }
        private readonly object _syncRoot = new object();

        public FixedRateTimer()
        { }

        public FixedRateTimer(INotifiable task, int delay, int interval)
        {
            Task = task;
            Delay = delay;
            Interval = interval;
        }

        public void Start()
        {
            lock (_syncRoot)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }

                _timer = new Timer((state) =>
                {
                    Task.Notify("pip-commons-timer");
                }, null, Delay, Interval);
                Started = true;
            }
        }

        public void Close(string correlationId)
        {
            lock (_syncRoot)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }
            }
        }
    }
}