using System.Collections.Generic;
using PipServices.Commons.Config;
using PipServices.Commons.Data;

namespace PipServices.Commons.Auth
{
    /// <summary>
    /// Credentials such as login and password, client id and key,
    /// certificates, etc. Separating credentials from connection parameters
    /// allow to store them in secure location and share among multiple connections.
    /// </summary>
    public class CredentialParams : ConfigParams
    {
        /// <summary>
        /// Creates an empty instance of credential parameters.
        /// </summary>
        public CredentialParams()
        {
        }

        /// <summary>
        /// Create an instance of credentials from free-form configuration map.
        /// </summary>
        /// <param name="map">a map with the credentials</param>
        public CredentialParams(IDictionary<string, string> map)
            : base(map)
        {
        }

        /// <summary>
        /// Checks if credential lookup shall be performed.
        /// The credentials are requested when 'store_key' parameter contains 
        /// a non-empty string that represents the name in credential store.
        /// </summary>
        public bool UseCredentialStore
        {
            get { return ContainsKey("store_key"); }
        }

        /// <summary>
        /// Gets or sets the key under which the connection shall be looked up in credential store. 
        /// </summary>
        public string StoreKey
        {
            get { return GetAsNullableString("store_key"); }
            set { this["store_key"] = value; }
        }

        /// <summary>
        /// Gets or sets the user name / login.
        /// </summary>
        public string Username
        {
            get { return GetAsNullableString("username"); }
            set { this["username"] = value; }
        }

        /// <summary>
        /// Gets or sets the service user password.
        /// </summary>
        public string Password
        {
            get { return GetAsNullableString("password"); }
            set { this["password"] = value; }
        }

        /// <summary>
        /// Gets or sets the client or access id
        /// </summary>
        public string AccessId
        {
            get
            {
                string accessId = GetAsNullableString("access_id");
                accessId = accessId ?? GetAsNullableString("client_id");
                return accessId;
            }
            set { this["access_id"] = value; }
        }

        /// <summary>
        /// Gets or sets the client or access key
        /// </summary>
        public string AccessKey
        {
            get
            {
                var accessKey = GetAsNullableString("access_key");
                accessKey = accessKey ?? GetAsNullableString("client_key");
                return accessKey;
            }
            set { this["access_key"] = value; }
        }

        public new static CredentialParams FromString(string line)
        {
            var map = StringValueMap.FromString(line);
            return new CredentialParams(map);
        }
    }
}
