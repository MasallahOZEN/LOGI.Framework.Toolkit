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

namespace Slf.Config
{
  /// <summary>
  /// Extends <see cref="ConfigurationElementCollection"/> in order
  /// to support a collection of <see cref="FactoryConfigurationElement"/> elements
  /// </summary>
  public class FactoryConfigurationCollection : ConfigurationElementCollection
  {
    /// <summary>
    /// Gets a <see cref="FactoryConfigurationElement"/> at a given index.
    /// </summary>
    public FactoryConfigurationElement this[int index]
    {
      get { return (FactoryConfigurationElement)BaseGet(index); }
    }

    /// <summary>
    /// Creates a new  <see cref="FactoryConfigurationElement"/> instance.
    /// </summary>
    protected override ConfigurationElement CreateNewElement()
    {
      return new FactoryConfigurationElement();
    }

    /// <summary>
    /// Adds the given element to the collection.
    /// </summary>
    /// <param name="element"></param>
    protected override void BaseAdd(ConfigurationElement element)
    {
      try
      {
        // check whether a factory config of this name was already added.
        bool add = true;
        FactoryConfigurationElement factoryConfigElement = element as FactoryConfigurationElement;
        foreach (FactoryConfigurationElement existingElement in this)
        {
          if (existingElement.Name.ToLowerInvariant() == factoryConfigElement.Name.ToLowerInvariant())
          {
            ILogger logger = LoggerService.GetDiagnosticLogger(GetType().FullName);
            logger.Warn("An error occured while loading the Factory Configuration elements. Please ensure that all factories have unique names.");
            add = false;
          }
        }

        if (add)
        {
          base.BaseAdd(element);
        }
      }
      catch (ConfigurationErrorsException e)
      {
        ILogger logger = LoggerService.GetDiagnosticLogger(GetType().FullName);
        logger.Warn(e, "An error occured while loading the Factory Configuration elements. Please ensure that all factories have unique names.");
      }
    }

    /// <summary>
    /// Gets the value of the property of the given <see cref="FactoryConfigurationElement"/>
    /// instance that has been designated as the key.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((FactoryConfigurationElement)element).Name;
    }
  }
}