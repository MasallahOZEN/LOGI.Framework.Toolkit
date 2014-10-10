using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Sessions
{
    internal class WindowsSession<T> : Session<T>
    {
        #region ISession<T> Members
        public override T GetValue(string session)
        {
            return (T)AppDomain.CurrentDomain.GetData(session);
        }
        public override void SetValue(string session, T value)
        {
            AppDomain.CurrentDomain.SetData(session, value);
        }
        public override void Clear(string session)
        {
            AppDomain.CurrentDomain.SetData(session, null);
        }
        public override void Flush()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
