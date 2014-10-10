using System;

namespace LOGI.Framework.Toolkit.Core.Exceptions
{
    ///<summary>
    /// InvalidOperationException
    ///</summary>
    public class ExcConvertionException:Base.BaseException
    {
        ///<summary>
        /// Default Constructor
        ///</summary>
        public ExcConvertionException():base()
        {
            
        }

        ///<summary>
        /// String parametreli Constructor 
        ///</summary>
        public ExcConvertionException(String message)
            : base(message)
        {

        }

        ///<summary>
        /// String ve Exception parametreli Constructor 
        ///</summary>
        public ExcConvertionException(String message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
