using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGI.Framework.Toolkit.Foundation.Session;

namespace LOGI.Framework.Toolkit.Core.Sessions
{
    public abstract class Session<T> : ISession<T>, ISessionKeyEnum<T>
    {
        #region ISession<T> Members
        public abstract T GetValue(string session);
        public abstract void SetValue(string session, T value);
        public abstract void Clear(string session);
        public abstract void Flush();
        #endregion

        #region ISessionKeyEnumable<T> Members
        public T GetValue(Enum session)
        {
            return GetValue(session.ToString());
        }
        public void SetValue(Enum session, T value)
        {
            SetValue(session.ToString(), value);
        }
        public void Clear(Enum session)
        {
            Clear(session.ToString());
        }
        #endregion
    }
}
