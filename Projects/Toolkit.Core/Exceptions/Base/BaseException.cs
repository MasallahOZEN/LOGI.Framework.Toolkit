
using Slf;
using LOGI.Framework.Toolkit.HelperLibrary.Constants;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using LOGI.Framework.Toolkit.Core.Extensions.ExtString;

namespace LOGI.Framework.Toolkit.Core.Exceptions.Base
{
    [Serializable]
    public abstract class BaseException : Exception, ISerializable
    {
        protected BaseException()
        {
        }

        protected BaseException(string message) : base(message)
        {
        }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected BaseException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public string ErrorHexCode
        {
            get
            {
                var typeName = this.GetType().FullName;
                int hashCode = typeName.GetHashCode();
                return string.Format("0x{0:X8}", hashCode);

            }
        }

        public string ErrorDecimalCode
        {
            get
            {
                var typeName = this.GetType().FullName;
                int hashCode = typeName.GetHashCode();

                return string.Format("{0}", hashCode);
            }
        }
    }
}

