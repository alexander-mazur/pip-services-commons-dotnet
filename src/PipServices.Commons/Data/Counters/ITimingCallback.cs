namespace PipServices.Commons.Counters
{
    public interface ITimingCallback
    {
        void EndTiming(string name, float elapsed);
    }
}
