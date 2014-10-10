using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LOGI.Framework.Toolkit.Core.Common.Collections
{
    [Serializable]
    [DataContract]
    public class TreeItemValue : IDisposable
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public bool Change { get; set; }

        [DataMember]
        public object Value { get; set; }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return string.Format("Key:{0},Change:{1},Value:{2}", Key, Change, Value.ToString());
        }
    }
}
