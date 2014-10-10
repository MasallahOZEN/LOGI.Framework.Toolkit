using System;
using System.Collections;
using System.Collections.Generic;

namespace LOGI.Framework.Toolkit.Core.Extensions.ExtDictionary
{
    ///<summary>
    /// ExtDictionary
    ///</summary>
    public static class Extensions
    {
        ///<summary>
        /// ValueOrSomethingElse
        /// XXX.ValueOrSomethingElse("Mark", d => d.ToString("MM/dd/yy"), () => "Unknown BirthDate"); 
        ///</summary>
        public static R ValueOrSomethingElse<K, V, R>(this Dictionary<K, V> Col, K Key, Func<V, R> Transform, Func<R> SomethingElse)
        {
            if (Col.ContainsKey(Key))
                return Transform(Col[Key]);

            return SomethingElse();
        }

        public static Dictionary<TKey, TElement> ToUniqueDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            {
                if (source == null) throw new ArgumentNullException("source");
                if (keySelector == null) throw new ArgumentNullException("keySelector");
                if (elementSelector == null) throw new ArgumentNullException("elementSelector");

                var d = new Dictionary<TKey, TElement>();
                foreach (TSource element in source)
                {
                    if (!d.ContainsKey(keySelector(element)))
                    {
                        d.Add(keySelector(element), elementSelector(element));
                    }
                }
                return d;
            }
        }

        /// <summary>
        /// Gets the safe value associated with the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        public static TValue GetSafeValue<TKey, TValue>(this Dictionary<TKey,
                             TValue> dictionary, TKey key)
        {
            return dictionary.GetSafeValue(key, default(TValue));
        }

        /// <summary>
        /// Gets the safe value associated with the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        public static TValue GetSafeValue<TKey, TValue>(this Dictionary<TKey,
               TValue> dictionary, TKey key, TValue defaultValue)
        {
            TValue result;
            if (!dictionary.TryGetValue(key, out result))
                result = defaultValue;
            return result;
        }

        public static IDictionary ToIDictionary(this DictionaryEntry[] tokens)
        {
            IDictionary tokenContext = new Hashtable();
            #region Import tokens
            if (tokens != null)
            {
                foreach (var dictionaryEntry in tokens)
                {
                    tokenContext.Add(dictionaryEntry.Key, dictionaryEntry.Value);
                }
            }
            #endregion

            return tokenContext;
        }
    }
}
