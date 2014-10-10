using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Foundation.Session
{
    public interface ISessionKeyEnum<T>
    {
        T GetValue(Enum session);
        void SetValue(Enum session, T value);
        void Clear(Enum session);
    }
}
