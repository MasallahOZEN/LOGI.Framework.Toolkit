namespace LOGI.Framework.Toolkit.Core.Configurations.Base
{
    ///<summary>
    /// Configuration Helper
    ///</summary>
    public class ConfigHelper
    {
        ///<summary>
        /// Config dosyasından data oku
        ///</summary>
        ///<param name="parameterName"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        public static T GetValue<T>(string parameterName)
        {
            var cBase = ConfigFactory.GetConfigManager();
            return cBase.GetValue<T>(parameterName);
        }


    }
}
