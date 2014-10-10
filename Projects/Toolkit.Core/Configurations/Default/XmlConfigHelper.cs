using System;
using System.Configuration;
using LOGI.Framework.Toolkit.Core.Configurations.Base;
using LOGI.Framework.Toolkit.Core.Extensions.ExtEnum;
using LOGI.Framework.Toolkit.Core.Extensions.ExtObject;

namespace LOGI.Framework.Toolkit.Core.Configurations.Default
{
    internal class XmlConfigHelper : ConfigBase
    {
        internal override object GetValue(string parameterName)
        {
            if (ConfigurationManager.AppSettings[parameterName] == null)
                throw new Exception(string.Format("Xml Config dosyasında geçersiz parametre adi. Parametre : {0}", parameterName));
            return ConfigurationManager.AppSettings[parameterName];

        }

        internal override T GetValue<T>(string parameterName)
        {
            if (ConfigurationManager.AppSettings[parameterName] == null)
                throw new Exception(string.Format("Xml Config dosyasında geçersiz parametre adi. Parametre : {0}", parameterName));
            return ConfigurationManager.AppSettings[parameterName].To<T>();

        }

        internal override void Encrypt(System.Configuration.Configuration confg, string section, ProtectionConfigurationProviders provider)
        {
            ConfigurationSection confStrSect = confg.GetSection(section);
            if (confStrSect != null)
            {
                confStrSect.SectionInformation.ProtectSection(provider.GetStringValue());
                confg.Save();
            }
        }

        internal override void Decrypt(System.Configuration.Configuration confg, string section)
        {
            ConfigurationSection confStrSect = confg.GetSection(section);
            if (confStrSect != null && confStrSect.SectionInformation.IsProtected)
            {
                confStrSect.SectionInformation.UnprotectSection();
                confg.Save();
            }
        }
    }
}
