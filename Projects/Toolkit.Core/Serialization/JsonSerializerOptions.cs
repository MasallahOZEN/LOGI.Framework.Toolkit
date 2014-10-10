using System;
using Newtonsoft.Json;
using LOGI.Framework.Toolkit.Foundation.Serialization;

namespace LOGI.Framework.Toolkit.Core.Serialization
{
    public class JsonSerializerOptions : ISerializerOptions
    {
        public JsonSerializerOptions()
        {
            JsonSerializerSettings=new JsonSerializerSettings();
        }
        public Formatting Formatting { get; set; }
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
    }
}
