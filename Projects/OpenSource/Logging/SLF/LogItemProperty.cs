using System;
using System.Collections.Generic;
using System.Text;

namespace Slf
{
    public class LogItemProperty
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public LogItemProperty()
        {

        }

        public LogItemProperty(string key,object value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"Key:{Key}-Value:{Value}";
        }
    }
}
