using System;

namespace PipServices.Commons.Count
{
    /**
     * Interface for Timing callbacks to record captured elapsed time
     */
    public interface ITimingCallback
    {
        /**
             * Recording calculated elapsed time 
             * @param name the name of the counter
             * @param elapsed time in milliseconds
             */
        void EndTiming(string name, double elapsed);
    }
}
