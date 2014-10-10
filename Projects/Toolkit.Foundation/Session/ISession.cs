using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Foundation.Session
{
    public interface ISession<T>
    {
        T GetValue(string session);
        void SetValue(string session, T value);
        void Clear(string session);
        void Flush();
    }
}
