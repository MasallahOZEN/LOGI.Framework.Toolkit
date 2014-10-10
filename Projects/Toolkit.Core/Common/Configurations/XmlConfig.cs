using System;
using System.Configuration;
using LOGI.Framework.Toolkit.Core.Extensions.ExtEnum;
using LOGI.Framework.Toolkit.Core.Extensions.ExtObject;
using LOGI.Framework.Toolkit.Core.Properties;
using LOGI.Framework.Toolkit.Foundation.Config;

namespace LOGI.Framework.Toolkit.Core.Common.Configurations
{
    ///<summary>
    /// XmlConfigHelper
    ///</summary>
    public class XmlConfig : IConfigWrapper 
    {
        public object Get(string parameterName,bool returnDefaultIfKeyNotExist=false)
        {
            var retValue = ConfigurationManager.AppSettings[parameterName];
            if (retValue==null && returnDefaultIfKeyNotExist == false)
            {
                throw new Exception(string.Format(Resources.XmlConfigHelperGecersizParametre, parameterName));
            }

            return ConfigurationManager.AppSettings[parameterName];

        }

        public T Get<T>(string parameterName, bool returnDefaultIfKeyNotExist = false)
        {

            var retValue = ConfigurationManager.AppSettings[parameterName];
            if (retValue == null && returnDefaultIfKeyNotExist == false)
            {
                throw new Exception(string.Format(Resources.XmlConfigHelperGecersizParametre, parameterName));
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[parameterName]))
            {
                return ConfigurationManager.AppSettings[parameterName].To<T>();    
            }

            return default(T);
        }

        public void Encrypt(Configuration confg, string section, ProtectionConfigurationProviders provider)
        {
            var confStrSect = confg.GetSection(section);
            if (confStrSect != null)
            {
                confStrSect.SectionInformation.ProtectSection(provider.GetStringValue());
                confg.Save();
            }
        }

        public void Decrypt(Configuration confg, string section)
        {
            var confStrSect = confg.GetSection(section);
            if (confStrSect != null && confStrSect.SectionInformation.IsProtected)
            {
                confStrSect.SectionInformation.UnprotectSection();
                confg.Save();
            }
        }
    }
}
