using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoundationBase.ConfigHelper
{
    public interface IConfigHelper
    {
        T GetValue<T>(string parameterName);
    }
}
