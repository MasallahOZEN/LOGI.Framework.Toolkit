using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using LOGI.Framework.Toolkit.Core.Extensions.ExtObject;

namespace LOGI.Framework.Toolkit.Core.Extensions.ExtXml
{
    ///<summary>
    /// Xml Extensions
    ///</summary>
    public static class Extensions
    {
        #region XmlNode
        ///<summary>
        /// GetNodeValue
        ///</summary>
        ///<param name="node"></param>
        ///<param name="attributeName"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        ///<exception cref="Exception"></exception>
        public static T GetNodeValue<T>(this XmlNode node, string attributeName)
        {
            if (node.Attributes[attributeName] == null)
                throw new Exception(string.Format("attribute bulunamadi : {0}", attributeName));
            return node.Attributes[attributeName].Value.To<T>();
        }

        ///<summary>
        /// GetNodeValue
        ///</summary>
        ///<param name="node"></param>
        ///<param name="attributeName"></param>
        ///<param name="defaultValue"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        public static T GetNodeValue<T>(this XmlNode node, string attributeName, T defaultValue)
        {
            try
            {
                return GetNodeValue<T>(node, attributeName);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static XElement GetXElement(this XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);
            return xDoc.Root;
        }

        public static XmlNode GetXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }

        /// <summary>
        /// Remove all the xml namespaces (xmlns) attributes in the xml string
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public static string RemoveAllXmlNamespace(this XmlDocument source, string xmlData)
        {
            string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";
            MatchCollection matchCol = Regex.Matches(xmlData, xmlnsPattern);

            foreach (Match m in matchCol)
            {
                xmlData = xmlData.Replace(m.ToString(), "");
            }
            return xmlData;
        }

        #endregion

        public static IEnumerable<XElement> ReadElements(this XmlReader r, string matchName)
        {
            //r.MoveToContent();
            while (r.Read())
            {
                switch (r.NodeType)
                {
                    case XmlNodeType.Element:
                        {
                            if (r.Name == matchName)
                            {
                                XElement el = XElement.ReadFrom(r) as XElement;
                                if (el != null)
                                    yield return el;
                            } break;
                        }
                }
            }
        }
    }
}
