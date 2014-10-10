using System;

namespace LOGI.Framework.Toolkit.Core.Exceptions.Base
{
    ///<summary>
    /// Uçuş servisi için base exception sınıfı
    ///</summary>
    public class BaseNotFoundException : BaseException
    {
        public BaseNotFoundException(string msg)
            : base(msg)//TODO:Resource'a al
        {
            
        }
    }
}
