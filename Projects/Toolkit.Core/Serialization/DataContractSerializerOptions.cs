using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using LOGI.Framework.Toolkit.Foundation.Serialization;

namespace LOGI.Framework.Toolkit.Core.Serialization
{
    public class DataContractSerializerOptions : ISerializerOptions
    {
        public DataContractSerializerOptions()
        {
            KnownTypes = new List<Type>();
        }
        public IEnumerable<Type> KnownTypes { get; set; }

        public string RootName { get; set; }
        public string RootNamespace { get; set; }
    }
}
