using System.Globalization;

namespace LOGI.Framework.Toolkit.HelperLibrary.CultureUtil
{
    public class Culture
    {
        ///<summary>
        /// ChangeCurrencyDecimalSeparator
        ///</summary>
        ///<param name="value"></param>
        ///<param name="fromCulture"></param>
        ///<param name="toCulture"></param>
        ///<returns></returns>
        public static string ChangeCurrencyDecimalSeparator(string value, CultureInfo fromCulture, CultureInfo toCulture)
        {
            var newCurrencyData = value.Replace(fromCulture.NumberFormat.CurrencyDecimalSeparator,
                toCulture.NumberFormat.CurrencyDecimalSeparator);
            return newCurrencyData;
        }
    }
}
