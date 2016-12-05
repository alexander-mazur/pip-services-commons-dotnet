namespace PipServices.Commons.Count
{
    /// <summary>
    /// Interface for Timing callbacks to record captured elapsed time
    /// </summary>
    public interface ITimingCallback
    {
        /// <summary>
        /// Recording calculated elapsed time 
        /// </summary>
        /// <param name="name">the name of the counter</param>
        /// <param name="elapsed">time in milliseconds</param>
        void EndTiming(string name, double elapsed);
    }
}
