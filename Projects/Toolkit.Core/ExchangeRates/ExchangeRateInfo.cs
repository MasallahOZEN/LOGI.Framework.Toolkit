using System;

namespace LOGI.Framework.Toolkit.Core.ExchangeRates
{
    [Serializable]
    public class ExchangeRateInfo
    {
        public bool IsVisible { get; set; }
        public int OrderNo { get; set; }
        public string Title { get; set; }
        public string CurrencyCode { get; set; }

    }
}
