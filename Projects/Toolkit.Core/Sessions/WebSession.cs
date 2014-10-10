using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Sessions
{
    internal class WebSession<T> : Session<T>
    {
        #region ISessionManager<T> Members
        public override T GetValue(string session)
        {
            object o = System.Web.HttpContext.Current.Session[session];
            if (o == null)
            {
                return default(T);
            }
            return (T)o;
        }
        public override void SetValue(string session, T value)
        {
            System.Web.HttpContext.Current.Session[session] = value;
        }
        public override void Clear(string session)
        {
            System.Web.HttpContext.Current.Session[session] = null;
        }
        public override void Flush()
        {
            System.Web.HttpContext.Current.Session.Abandon();
        }
        #endregion
    }
}
