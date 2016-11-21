using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public class FixedRateTimer : IClosable
    {
        private Timer _timer;
        private readonly object _syncRoot = new object();

        public FixedRateTimer() { }

        public FixedRateTimer(INotifiable task, int interval, int delay)
        {
            Task = task;
            Interval = interval;
            Delay = delay;
        }

        public INotifiable Task { get; set; }
        public int Delay { get; set; }
        public int Interval { get; set; }
        public bool IsStarted { get; private set; }

        public void Start()
        {
            lock (_syncRoot)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }

                _timer = new Timer(
                    (state) => 
                    {
                        Task.NotifyAsync("pip-commons-timer");
                    }, 
                    null, Delay, Interval
                );
                IsStarted = true;
            }
        }

        public async Task CloseAsync(string correlationId)
        {
            lock (_syncRoot)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }
            }

            await System.Threading.Tasks.Task.Delay(0);
        }
    }
}