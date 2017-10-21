using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using LOGI.Framework.Toolkit.Core.Extensions.ExtException;
using Newtonsoft.Json;
using ProtoBuf;
using JsonSerializer = LOGI.Framework.Toolkit.Core.Serialization.JsonSerializer;

namespace LOGI.Framework.Toolkit.Core.Common.Collections
{
    [Serializable]
    [DataContract]
    public class TreeItem : IDisposable, IXmlSerializable
    {
        [DataMember]
        public string ValueType { get; set; }

        [DataMember]
        public string Key { get; set; }

        private object _value;

        [DataMember]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (_value == null)
                {
                    ValueType = string.Empty;
                }
                else
                {
                    ValueType = _value.GetType().AssemblyQualifiedName;
                }
            }
        }

        public Type GetValueType()
        {
            return Type.GetType(ValueType);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this); 
        }

        public override string ToString()
        {
            return string.Format("Key:{0},Value:{1}", Key, Value.ToString());
        }

        #region IXmlSerializable
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            Key = reader.ReadElementString("Key");

            reader.Read();
            var deserializeVal = reader.ReadElementString("Value");

            reader.Read();
            ValueType = reader.ReadElementString("ValueType");

            try
            {
                var serializer = new Core.Serialization.JsonSerializer();
                var serializerObj = new Core.Serialization.JsonSerializerOptions();
                serializerObj.JsonSerializerSettings.TypeNameHandling=TypeNameHandling.All;

                Value = serializer.DeSerialize(GetValueType(), deserializeVal, serializerObj);
            }
            catch (Exception exp)
            {
            }


        }

        public void WriteXml(XmlWriter writer)
        {
            var serializedValue = "";

            try
            {
                var serializerObj = new Core.Serialization.JsonSerializerOptions();
                serializerObj.JsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;

                serializedValue = new JsonSerializer().Serialize(Value, serializerObj);
            }
            catch (Exception exp)
            {
                serializedValue = string.Format("Error:{0}", exp.GetInnerException());
            }

            writer.WriteElementString("Key", Key);
            writer.WriteElementString("Value", serializedValue);
            writer.WriteElementString("ValueType", ValueType);
        } 
        #endregion
    }
}
