using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoundationBase.CacheProvider
{
    public interface ICacheProvider
    {
        void Save<T>(String key, T data);
        void Remove(String key);
        T Get<T>(String key);
    }
}
