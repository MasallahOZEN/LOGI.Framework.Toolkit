using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Slf;
using LOGI.Framework.Toolkit.Core.ExchangeRates;
using LOGI.Framework.Toolkit.Foundation.Globalization;
using LOGI.Framework.Toolkit.Core.Extensions.ExtObject;
using LOGI.Framework.Toolkit.HelperLibrary.Constants;
using LOGI.Framework.Toolkit.Core.Extensions.ExtString;
using LOGI.Framework.Toolkit.OpenSource.AspectF;

namespace LOGI.Framework.Toolkit.Core.ExchangeRates.Provider.IMKB
{
    public class IMKBExchangeRate : ICurrencyConverter
    {
        private ILogger Logger=LoggerService.GetLogger(ConstantHelper.Logging.Log2ExchangeRate);
        #region Constructors

        public IMKBExchangeRate()
        {
            
        }

        #endregion

        #region ICurrencyConverter implementations
        public decimal ConvertCurrency(decimal currencyValue, string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency)
            {
                return currencyValue;
            }
            //TODO:servis sorunlarında ne dönecek
            var returnValue = default(decimal);

            returnValue=Let.Us.
                Cache<decimal>(string.Format("currencyValue:{0} - fromCurrency:{1} - toCurrency:{2}", currencyValue,
                                           fromCurrency, toCurrency)).
                Return(() =>
                {
                    var date = DateTime.Today;

                    var rateList = GetExchangeRateAll(date);

                    //Türk lirası kur sembollerinden birinin gelmesi durumunda IMKB'den dönmeyecek olan bu değerlerin aşağıda null oluşmaması için ekleniyor
                    rateList.ExchangeRateItems.Add(new ExchangeRateItem { BanknoteBuying = 1, CurrencyCode = "TRY", BanknoteSelling = 1, ForexBuying = 1, ForexSelling = 1 });
                    rateList.ExchangeRateItems.Add(new ExchangeRateItem { BanknoteBuying = 1, CurrencyCode = "TL", BanknoteSelling = 1, ForexBuying = 1, ForexSelling = 1 });
                    rateList.ExchangeRateItems.Add(new ExchangeRateItem { BanknoteBuying = 1, CurrencyCode = "YTL", BanknoteSelling = 1, ForexBuying = 1, ForexSelling = 1 });

                    #region Get Currency Values
                    //data döndümü);
                    if (rateList.ExchangeRateItems == null || rateList.ExchangeRateItems.Count < 1)
                    {
                        LogError(currencyValue, fromCurrency, toCurrency, "IMKB den data dönmedi !");

                        return returnValue;
                    }

                    //dönen datadan fromCurrency sorgula
                    var fromCurrencyRateItem =
                        rateList.ExchangeRateItems.Where(rate => rate.CurrencyCode == fromCurrency).FirstOrDefault();

                    //fromCurrency geldimi
                    if (fromCurrencyRateItem == null)
                    {
                        LogError(currencyValue, fromCurrency, toCurrency, string.Format("[{0}] Döviz kuru IMKB servisten dönmedi !", fromCurrency));

                        return returnValue;
                    }

                    var fromCurrencyValue = fromCurrencyRateItem.ForexSelling;//TODO: hangi değer alınacak

                    //dönen datadan toCurrency sorgula
                    var toCurrencyRateItem =
                        rateList.ExchangeRateItems.Where(rate => rate.CurrencyCode == toCurrency).FirstOrDefault();

                    //toCurrency geldimi
                    if (toCurrencyRateItem == null)
                    {
                        LogError(currencyValue, fromCurrency, toCurrency, string.Format("[{0}] Döviz kuru IMKB servisten dönmedi !", toCurrency));

                        return returnValue;
                    }

                    var toCurrencyValue = toCurrencyRateItem.ForexSelling;//TODO: hangi değer alınacak 

                    decimal relativeValue = (fromCurrencyValue / toCurrencyValue).To<decimal>(defaultValue:0);
                    decimal convertedPrice = 0;

                    convertedPrice = (currencyValue * relativeValue);

                    return convertedPrice;
                    #endregion
                });

            return returnValue;

            
        }

        #endregion

        #region Customs

        internal string GetExchangeRateValue(DateTime date, ExchangeRateCurrencyCodes code, ExchangeOperations operation)
        {
            return DovizIslemleri.KurDegerAl(DateTime.Today, code, operation);
        }

        private List<ExchangeRateItem> GetRateItemsFromXml(IEnumerable<XElement> queryableObj)
        {
            if (queryableObj == null)
            {
                return new List<ExchangeRateItem>();
            }
            IEnumerable<ExchangeRateItem> _ExchangeRateItems = from rateItem in queryableObj
                                                               #region Linq variables
                                                               let banknoteBuying = rateItem.Element("BanknoteBuying") != null && rateItem.Element("BanknoteBuying").Value.IsNotNullOrNotEmpty() ?
                                                                    rateItem.Element("BanknoteBuying").Value.ToStringCultureSeparatorFormat().To<float>(returnDefaultValue: true) : 0

                                                               let banknoteSelling = rateItem.Element("BanknoteSelling") != null && rateItem.Element("BanknoteSelling").Value.IsNotNullOrNotEmpty() ?
                                                                    rateItem.Element("BanknoteSelling").Value.ToStringCultureSeparatorFormat().To<float>(returnDefaultValue: true) : 0

                                                               let forexBuying = rateItem.Element("ForexBuying") != null && rateItem.Element("ForexBuying").Value.IsNotNullOrNotEmpty() ?
                                                                   rateItem.Element("ForexBuying").Value.ToStringCultureSeparatorFormat().To<float>(returnDefaultValue: true) : 0

                                                               let forexSelling = rateItem.Element("ForexSelling") != null && rateItem.Element("ForexSelling").Value.IsNotNullOrNotEmpty() ?
                                                                   rateItem.Element("ForexSelling").Value.ToStringCultureSeparatorFormat().To<float>(returnDefaultValue: true) : 0

                                                               let currencyCode = rateItem.Attribute("CurrencyCode") != null ?
                                                                    rateItem.Attribute("CurrencyCode").Value : string.Empty
                                                               let currencyTitle = rateItem.Element("CurrencyName") != null ?
                                                                    rateItem.Element("CurrencyName").Value : string.Empty
                                                               let currencyDesc = rateItem.Element("Isim") != null ?
                                                                    rateItem.Element("Isim").Value : string.Empty 
                                                               #endregion

                                                               select new ExchangeRateItem
                                                               {
                                                                   CurrencyTitle = currencyTitle,
                                                                   CurrencyDescription = currencyDesc,
                                                                   BanknoteBuying = banknoteBuying,
                                                                   BanknoteSelling = banknoteSelling,
                                                                   ForexBuying = forexBuying,
                                                                   ForexSelling = forexSelling,
                                                                   CurrencyCode = currencyCode
                                                               };

            List<ExchangeRateItem> returnVal = _ExchangeRateItems.OrderBy(exc => exc.CurrencyTitle).ToList();
            if (returnVal == null)
            {
                returnVal = new List<ExchangeRateItem>();
            }
            else
            {
                for (int i = 1; i < returnVal.Count; i++)
                {
                    returnVal[i - 1].OrderNo = i;
                }
            }
            return returnVal;
        }

        private ExchangeRate GetExchangeRateAll(DateTime date)
        {
            ExchangeRate newRateList = new ExchangeRate();

            XDocument rates = DovizIslemleri.TumKurlar(date);

            if (rates != null)
            {
                if (rates.Element("Tarih_Date") != null)
                {
                    if (rates.Element("Tarih_Date").Attribute("Date") != null)
                    {
                        string dateValue = rates.Element("Tarih_Date").Attribute("Date").Value;
                        try
                        {
                            int month = dateValue.Substring(0, 2).To<int>();
                            int day = dateValue.Substring(3, 2).To<int>();
                            int year = dateValue.Substring(6, 4).To<int>();

                            DateTime newDate = new DateTime(year, month, day);

                            newRateList.LastUpdate = newDate;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                newRateList.ExchangeRateItems = GetRateItemsFromXml(rates.Descendants("Currency").AsEnumerable());

            }

            return newRateList;
        }

        private void LogError(decimal currencyValue, string fromCurrency, string toCurrency, string messageStr)
        {
            //TODO: resource üzerine al
            Logger.Error(string.Format("CurrencyValue:{0} - FromCurrency:{1} - ToCurrency:{2} --> {3}", currencyValue, fromCurrency, toCurrency, messageStr));
        }
        #endregion
    }
}
