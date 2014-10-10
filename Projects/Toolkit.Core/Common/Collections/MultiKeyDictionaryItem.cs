using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Common.Collections
{
    public class MultiKeyDictionaryItem<TKey,TValue>
    {
        public MultiKeyDictionaryItem(TKey key,TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public override string ToString()
        {
            return string.Format("Key: {0} - Value: {1}",this.Key.ToString(),this.Value.ToString());
        }
    }
}
