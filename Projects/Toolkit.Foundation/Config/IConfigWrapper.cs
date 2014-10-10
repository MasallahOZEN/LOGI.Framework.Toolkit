namespace LOGI.Framework.Toolkit.Foundation.Config
{
    ///<summary>
    /// IConfigWrapper
    ///</summary>
    public interface IConfigWrapper
    {
        object Get(string parameterName, bool returnDefaultIfKeyNotExist = false);
        T Get<T>(string parameterName, bool returnDefaultIfKeyNotExist = false);
        void Encrypt(System.Configuration.Configuration configuration, string section, ProtectionConfigurationProviders provider);
        void Decrypt(System.Configuration.Configuration configuration, string section); 
        
    }

    ///<summary>
    /// ProtectionConfigurationProviders
    ///</summary>
    public enum ProtectionConfigurationProviders
    {
        ///<summary>
        /// Data Protection Configuration Provider
        ///</summary>
        DataProtectionConfigurationProvider,

        ///<summary>
        /// RSA Protected Configuration Provider
        ///</summary>
        RsaProtectedConfigurationProvider
    }

}
