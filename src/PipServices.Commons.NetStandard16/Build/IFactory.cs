namespace PipServices.Commons.Build
{
    public interface IFactory
    {
        /// <summary>
        /// Determines whether this instance can create object by specified locater.
        /// </summary>
        /// <param name="locater">The locater.</param>
        /// <returns><c>true</c> if this instance can create the specified locater; otherwise, <c>false</c>.</returns>
        bool CanCreate(object locater);
        /// <summary>
        /// Creates the specified locater.
        /// </summary>
        /// <param name="locater">The locater.</param>
        /// <returns>System.Object.</returns>
        object Create(object locater);
    }
}