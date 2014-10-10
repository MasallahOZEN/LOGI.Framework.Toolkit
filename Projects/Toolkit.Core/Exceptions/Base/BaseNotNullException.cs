using System;

namespace LOGI.Framework.Toolkit.Core.Exceptions.Base
{
    ///<summary>
    /// Uçuş servisi request için null exception sınıfı
    ///</summary>
    public class BaseNotNullException : BaseException
    {
        public BaseNotNullException()
            : base("")
        {
            
        }

        public BaseNotNullException(string msg)
            : base(string.Format("Null Olamaz...->{0}", msg))//TODO:Resource'a al
        {

        }
    }
}
