using System;

namespace LOGI.Framework.Toolkit.Core.ExchangeRates
{
    [Serializable]
    public class ExchangeRateItem
    {
        public string CurrencyCode { get; set; }
        public string CurrencyTitle { get; set; }
        public string CurrencyDescription { get; set; }
        public float ForexBuying { get; set; }
        public float ForexSelling { get; set; }
        public float BanknoteBuying { get; set; }
        public float BanknoteSelling { get; set; }
        public bool IsVisible { get; set; }
        public int OrderNo { get; set; }
    }
}
