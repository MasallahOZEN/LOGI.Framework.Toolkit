using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Common.Collections
{
    public class MultiKeyDictionary<TKey,TValue> : BusinessObjectCollection<MultiKeyDictionaryItem<TKey,TValue>>
    {
        public new void Add(TKey key,TValue value)
        {
            if (this._innerArray==null)
            {
                this._innerArray=new List<MultiKeyDictionaryItem<TKey, TValue>>();
            }
            this._innerArray.Add(new MultiKeyDictionaryItem<TKey, TValue>(key,value));
        }
    }
}
