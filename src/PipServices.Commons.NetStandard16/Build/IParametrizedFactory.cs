namespace PipServices.Commons.Build
{
    /// <summary>
    /// Interface IParametrizedFactory
    /// Provides creation method with parameter
    /// </summary>
    /// <seealso cref="PipServices.Commons.Build.IFactory" />
    public interface IParametrizedFactory : IFactory
    {
        /// <summary>
        /// Creates the object with parameter by specified locater.
        /// </summary>
        /// <param name="locater">The locater.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>System.Object.</returns>
        object Create(object locater, object parameter);
    }
}
