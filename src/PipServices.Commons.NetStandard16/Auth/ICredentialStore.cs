using System.Threading.Tasks;

namespace PipServices.Commons.Auth
{
    /// <summary>
    /// Store that keeps and located client credentials.
    /// </summary>
    public interface ICredentialStore
    {
        /// <summary>
        /// Stores credential in the store
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="key">the key to lookup credential</param>
        /// <param name="credential">a credential parameters</param>
        Task StoreAsync(string correlationId, string key, CredentialParams credential);

        /// <summary>
        /// Looks up credential from the store
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="key">a key to lookup credential</param>
        /// <returns>found credential parameters or <code>null</code> if nothing was found</returns>
        Task<CredentialParams> LookupAsync(string correlationId, string key);
    }
}
