using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Slf;
using LOGI.Framework.Toolkit.Core.Client;
using LOGI.Framework.Toolkit.Foundation.Globalization;
using LOGI.Framework.Toolkit.HelperLibrary.Constants;
using LOGI.Framework.Toolkit.Core.Extensions.ExtString;
using LOGI.Framework.Toolkit.OpenSource.AspectF;
using LOGI.Framework.TravelPortal.Core.Model.Common;

namespace LOGI.Framework.Toolkit.Core.ExchangeRates.Provider.FromDatabase
{
    public class FromDatabaseExchangeRate : ICurrencyConverter
    {
        private ILogger Logger=LoggerService.GetLogger(ConstantHelper.Logging.Log2ExchangeRate);
        private static object sharedObj=new object();
        #region Constructors

        public FromDatabaseExchangeRate()
        {
            
        }

        #endregion

        #region ICurrencyConverter implementations
        public decimal ConvertCurrency(decimal currencyValue, string fromCurrency, string toCurrency)
        {
            var listTLCurrency=new List<string>(){{"YTL"},{"TL"},{"TRY"},{"TRL"}};

            if (fromCurrency==toCurrency || (listTLCurrency.Contains(fromCurrency) && listTLCurrency.Contains(toCurrency)))
            {
                return currencyValue;
            }
            var returnValue = default(decimal);

            Let.Cache = () => Proxies.Instance.CacheWrapper;
            returnValue = Let.Us.
                //Cache<float>(string.Format("currencyValue:{0} - fromCurrency:{1} - toCurrency:{2}", currencyValue,
                //                           fromCurrency, toCurrency)).
                Return(() =>
                           {
                               //TODO:servis sorunlarında ne dönecek

                               var fromCurrencyDbRec =GetCurrencyObjectFromDb(fromCurrency);

                               if (fromCurrencyDbRec == null || fromCurrencyDbRec.CurrencyCode.IsNullOrEmpty() ||
                                   !fromCurrencyDbRec.RelativeValue.HasValue)
                               {
                                   LogError(currencyValue, fromCurrency, toCurrency,
                                            string.Format("{0} için DB'den RelativeValue alınamadı !", fromCurrency));
                                   //TODO: resource
                                   return 0;
                               }

                               var toCurrencyDbRec = GetCurrencyObjectFromDb(toCurrency);

                               if (toCurrencyDbRec == null || toCurrencyDbRec.CurrencyCode.IsNullOrEmpty() ||
                                   !toCurrencyDbRec.RelativeValue.HasValue)
                               {
                                   LogError(currencyValue, fromCurrency, toCurrency,
                                            string.Format("{0} için DB'den RelativeValue alınamadı !", toCurrency));
                                   //TODO: resource
                                   return 0;
                               }

                               var relativeValue = fromCurrencyDbRec.RelativeValue.Value/
                                                   toCurrencyDbRec.RelativeValue.Value;
                               decimal convertedPrice = 0;

                               convertedPrice = (currencyValue* relativeValue);

                               return convertedPrice;
                           });

            return returnValue;
        }
        #endregion

        #region Customs
        private void LogError(decimal currencyValue, string fromCurrency, string toCurrency, string messageStr)
        {
            //TODO: resource üzerine al
            Logger.Error(string.Format("FromDatabaseExchangeRate=>CurrencyValue:{0} - FromCurrency:{1} - ToCurrency:{2} --> {3}", currencyValue, fromCurrency, toCurrency, messageStr));
        }

        private Currency GetCurrencyObjectFromDb(string currencyCode)
        {

            var retValue = Let.Us.
                //Cache<Currency>(Proxies.Instance.CacheWrapper,
                //                string.Format("FromDatabaseExchangeRate-CurrencyCode:{0}", currencyCode)).//TODO:cache key lerini bir yerden al
                Return(() =>
                {
                    try
                    {
                        Monitor.Enter(sharedObj);
                        var currencyDbContext = Proxies.Instance.CurrencyRepoContext;
                        var currencyObj = currencyDbContext.GetQuery().Where(
                            cur => cur.CurrencyCode == currencyCode).
                            FirstOrDefault();
                        return currencyObj;
                    }
                    finally
                    {
                        Monitor.Exit(sharedObj);
                    }
                });

            return retValue;
        }
        #endregion
    }
}
