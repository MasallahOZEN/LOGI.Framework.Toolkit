// SLF.NET - Simple Logging Façade for .NET
// Contact and Information: http://slf.codeplex.com
//
// This library is free software; you can redistribute it and/or
// modify it in any way you see fit as long as this copyright
// notice is not being removed.
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE


using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Slf.Config
{
  /// <summary>
  /// The factory element within the application
  /// configuration.
  /// </summary>
  public class FactoryConfigurationElement : ConfigurationElement
  {
    private const string NameAttribute = "name";

    private const string TypeAttribute = "type";

    private const string FactoryDataElement = "factory-data";

    /// <summary>
    /// Gets custom factory data, if available. This is data of the factory
    /// element, that is wrapped into an element named <c>factory-data</c>.
    /// The contents of the <c>factory-data</c> element can be as simple as
    /// a literal, or a complex XML element.
    /// Defaults to null.
    /// </summary>
    /// <example>
    /// The following example shows two possible configurations - one with
    /// a literal as custom configuration data, one with XML based configuration:
    /// <code>
    /// &lt;slf>
    ///   &lt;!-- a factory configuration with literal content -->
    ///   &lt;factory name="simple">
    ///     &lt;factory-data>
    ///       simple data
    ///     &lt;/factory-data>
    ///   &lt;/factory>
    /// 
    ///   &lt;!-- a factory configuration with XML sub elements -->
    ///   &lt;factory name="complex">
    ///     &lt;factory-data>
    ///       &lt;child foo="bar">
    ///         &lt;subchild>foobar&lt;/subchild>
    ///       &lt;/child>
    ///     &lt;/factory-data>
    ///   &lt;/factory>
    /// &lt;/slf>
    /// </code>
    /// </example>
    public string FactoryData { get; set; }

    /// <summary>
    /// Indicates the logger name which resolves to this factory
    /// </summary>
    [ConfigurationProperty(NameAttribute, IsRequired = false)]
    public string Name
    {
      get
      {
        return (string)this[NameAttribute];
      }
      set
      {
        this[NameAttribute] = value;
      }
    }

    /// <summary>
    /// Indicates the named factory Type
    /// </summary>
    [ConfigurationProperty(TypeAttribute, IsRequired = false)]
    public string Type
    {
      get
      {
        return (string)this[TypeAttribute];
      }
      set
      {
        this[TypeAttribute] = value;
      }
    }


    /// <summary>
    /// Parses custom factory data into the <see cref="FactoryData"/> property,
    /// if <paramref name="elementName"/> refers to a <c>factory-data</c> element.
    /// </summary>
    /// <param name="elementName"></param>
    /// <param name="reader"></param>
    /// <returns>True if the element is a (supported) <c>factory-data</c> element.
    /// If it's an other unexpected element, this method returns false.</returns>
    protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
    {
      if (elementName != FactoryDataElement)
      {
        //we really don't know that element - delegate to base class (just returns false)
        return base.OnDeserializeUnrecognizedElement(elementName, reader);
      }

      //parse the contents of the element
      //(the string does not contain the "factory-data" element markup)
      FactoryData = reader.ReadInnerXml().Trim();
      return true;
    }
  }
}
