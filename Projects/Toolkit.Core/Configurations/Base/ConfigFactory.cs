using LOGI.Framework.Toolkit.Core.Configurations.Default;

namespace LOGI.Framework.Toolkit.Core.Configurations.Base
{
    internal class ConfigFactory
    {
        internal static ConfigBase GetConfigManager()
        {
            return new XmlConfigHelper();

        }
    }
}
