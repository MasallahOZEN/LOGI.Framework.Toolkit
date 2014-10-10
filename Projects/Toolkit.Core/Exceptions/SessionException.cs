using System;

namespace LOGI.Framework.Toolkit.Core.Exceptions
{
    ///<summary>
    /// InvalidOperationException
    ///</summary>
    public class SessionException:Exception
    {
        ///<summary>
        /// Default Constructor
        ///</summary>
        public SessionException():base()
        {
            
        }

        ///<summary>
        /// String parametreli Constructor 
        ///</summary>
        public SessionException(String message)
            : base(message)
        {

        }

        ///<summary>
        /// String ve Exception parametreli Constructor 
        ///</summary>
        public SessionException(String message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
