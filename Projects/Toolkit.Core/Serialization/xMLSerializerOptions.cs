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
    public class XMLSerializerOptions : ISerializerOptions
    {
        public XMLSerializerOptions()
        {
            ExtraTypes = new Type[]{};
        }
        public Type[] ExtraTypes { get; set; }
    }
}
