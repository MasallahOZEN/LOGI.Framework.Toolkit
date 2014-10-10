using System;
using System.Collections.Generic;

namespace LOGI.Framework.Toolkit.Core.ExchangeRates
{
    public class ExchangeRate
    {
        private List<ExchangeRateItem> _ExchangeRateItems;

        public List<ExchangeRateItem> ExchangeRateItems
        {
            get
            {
                if (_ExchangeRateItems == null)
                {
                    return new List<ExchangeRateItem>();
                }
                else
                {
                    return _ExchangeRateItems;
                }
            }
            set
            {
                _ExchangeRateItems = value;
            }
        }

        public DateTime LastUpdate { get; set; }
    }
}
