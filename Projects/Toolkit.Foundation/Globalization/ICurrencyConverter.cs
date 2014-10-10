using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Foundation.Globalization
{
    /// <summary>
    /// ICurrencyConverter
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Para birimleri arasında dönüşüm sağlar
        /// </summary>
        /// <param name="fromCurrency">Hangi para biriminden( örn TRY)</param>
        /// <param name="toCurrency">Hangi para birimine (örn. EUR)</param>
        /// <returns>Float</returns>
        decimal ConvertCurrency(decimal currencyValue, string fromCurrency, string toCurrency);
        /*
        /// <summary>
        /// Para birimleri arasında dönüşüm sağlar
        /// </summary>
        /// <param name="fromCurrency">Hangi para biriminden( örn TRY)</param>
        /// <param name="toCurrency">Hangi para birimine (örn. EUR)</param>
        /// <returns>Float</returns>
        decimal ConvertCurrency(decimal currencyValue, string fromCurrency, string toCurrency);

        */
    }
}
