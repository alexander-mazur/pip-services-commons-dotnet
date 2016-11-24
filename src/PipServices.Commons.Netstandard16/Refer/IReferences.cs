using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    /// <summary>
    /// Set of component references with abilities to add new references, find reference using locators
    /// or remove reference from the set
    /// </summary>
    public interface IReferences
    {
        /// <summary>
        /// Puts a new component reference to the set
        /// </summary>
        /// <param name="reference">a component reference to be added that implements ILocateable interface</param>
        void Put(object reference);

        /// <summary>
        /// Puts a new component reference to the set with explicit locator
        /// </summary>
        /// <param name="locator">a locator to find the reference</param>
        /// <param name="reference">a component reference to be added</param>
        void Put(object locator, object reference);

        /// <summary>
        /// Removes component reference from the set.
        /// The method removes only the last reference.
        /// </summary>
        /// <param name="locator">a locator to find the reference to remove</param>
        /// <returns>a removed reference</returns>
        object Remove(object locator);

        /// <summary>
        /// Gets all stored component references
        /// </summary>
        /// <returns>a list with component references</returns>
        List<object> GetAll();

        /// <summary>
        /// Gets a list of component references that match provided locator
        /// </summary>
        /// <param name="locator">a locator to find references</param>
        /// <returns>a list with found component references</returns>
        List<object> GetOptional(object locator);

        /// <summary>
        /// Gets a list of component references that match provided locator
        /// and matching to the specified type.
        /// </summary>
        /// <param name="locator">a locator to find references</param>
        /// <returns>a list with found component references</returns>
        List<T> GetOptional<T>(object locator);

        /// <summary>
        /// Gets a list of component references that match provided locator.
        /// If no references found an exception is thrown
        /// </summary>
        /// <param name="locator">a locator to find references</param>
        /// <returns>a list with found component references</returns>
        List<object> GetRequired(object locator);

        /// <summary>
        /// Gets a list of component references that match provided locator
        /// and matching to the specified type.
        /// If no references found an exception is thrown
        /// </summary>
        /// <param name="locator">a locator to find references</param>
        /// <returns>a list with found component references</returns>
        List<T> GetRequired<T>(object locator);

        /// <summary>
        /// Gets a component references that matches provided locator.
        /// The search is performed from latest added references.
        /// </summary>
        /// <param name="locator">a locator to find a reference</param>
        /// <returns>a found component reference or <code>null</code> if nothing was found</returns>
        object GetOneOptional(object locator);

        /// <summary>
        /// Gets a component references that matches provided locator
        /// and matching to the specified type.
        /// The search is performed from latest added references.
        /// </summary>
        /// <param name="locator">a locator to find a reference</param>
        /// <returns>a found component reference or <code>null</code> if nothing was found</returns>
        T GetOneOptional<T>(object locator);

        /// <summary>
        /// Gets a component references that matches provided locator.
        /// The search is performed from latest added references.
        /// </summary>
        /// <param name="locator">a locator to find a reference</param>
        /// <returns>a found component reference</returns>
        object GetOneRequired(object locator);

        /// <summary>
        /// Gets a component references that matches provided locator
        /// and matching to the specified type
        /// The search is performed from latest added references.
        /// </summary>
        /// <param name="locator">a locator to find a reference</param>
        /// <returns>a found component reference</returns>
        T GetOneRequired<T>(object locator);

        /// <summary>
        /// Gets a component references that matches provided locator.
        /// </summary>
        /// <param name="reference">a component reference to start the search and continue form latest to oldest</param>
        /// <param name="locator">a locator to find a reference</param>
        /// <returns>a found component reference</returns>
        object GetOneBefore(object reference, object locator);

        /// <summary>
        /// Gets a component references that matches provided locator
        /// and matching to the specified type
        /// </summary>
        /// <param name="reference">a component reference to start the search and continue form latest to oldest</param>
        /// <param name="locator">a locator to find a reference</param>
        /// <returns>a found component reference</returns>
        T GetOneBefore<T>(object reference, object locator);
    }
}