using LOGI.Framework.Toolkit.Core.Exceptions.Base;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace LOGI.Framework.Toolkit.Core.Exceptions
{
    [Serializable]
    public class LoginFailedException : Base.BaseException
    {
        public LoginFailedException()
            : base("Login Failed")
        {
        }

        public LoginFailedException(string message)
            : base("<Login Failed> " + message)
        {
        }

        protected LoginFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public LoginFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
    }
}

