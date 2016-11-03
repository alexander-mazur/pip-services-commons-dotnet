namespace PipServices.Commons.Refer
{
    /// <summary>
    /// Interface for objects that return a <see cref="Descriptor"/>.
    /// </summary>
    public interface IDescriptable
    {
        /// <summary>
        /// Gets the component descriptor.
        /// </summary>
        /// <returns>The component <see cref="Descriptor"/></returns>
        Descriptor GetDescriptor();
    }
}
