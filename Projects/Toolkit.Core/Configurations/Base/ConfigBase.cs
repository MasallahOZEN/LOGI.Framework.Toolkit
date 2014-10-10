using LOGI.Framework.Toolkit.Core.Enums;

namespace LOGI.Framework.Toolkit.Core.Configurations.Base
{
    ///<summary>
    /// ProtectionConfigurationProviders
    ///</summary>
    public enum ProtectionConfigurationProviders
	{
        ///<summary>
        /// Data Protection Configuration Provider
        ///</summary>
        [StringValue("DataProtectionConfigurationProvider")]
        DataProtectionConfigurationProvider,

        ///<summary>
        /// RSA Protected Configuration Provider
        ///</summary>
        [StringValue("RSAProtectedConfigurationProvider")]
        RsaProtectedConfigurationProvider
	}

    internal abstract class ConfigBase
    {
        internal abstract object GetValue(string parameterName);
        internal abstract T GetValue<T>(string parameterName);
        internal abstract void Encrypt(System.Configuration.Configuration configuration, string section, ProtectionConfigurationProviders provider);
        internal abstract void Decrypt(System.Configuration.Configuration configuration, string section);
    }
}
