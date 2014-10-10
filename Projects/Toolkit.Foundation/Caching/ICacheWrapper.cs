using System;
using System.Collections.Generic;


namespace LOGI.Framework.Toolkit.Foundation.Caching
{
    ///<summary>
    /// ICacheWrapper
    ///</summary>
    public interface ICacheWrapper
    {
        
        T Get<T>(String key);
        bool Exist(String key);
        IEnumerable<T> Find<T>(Func<T, bool > predicate);
        bool Save<T>(String key, T data,TimeSpan? timeout=null,bool overriteIfExist=false);
        bool Remove(String key);
        bool Remove<T>(Func<T, bool > predicate);
        bool Clear();
        
    }
}
