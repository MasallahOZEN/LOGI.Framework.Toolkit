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
using System.Xml;


namespace Slf.Factories
{
  /// <summary>
  /// This factory sub-classes
  /// <see cref="SimpleLoggerFactory"/> adding configuration support via
  /// the <see cref="IConfigurableLoggerFactory"/> interface.<br/>
  /// Keep in mind that using this factory disables support for
  /// named loggers - it will always return the same logger instance,
  /// no matter what logger name is submitted. Furthermore, this factory
  /// will *NOT* set the <see cref="ILogger.Name"/> property
  /// of the logger it creates, so the name will return the logger's
  /// default value.s
  /// </summary>
  /// 
  /// <example>
  /// This factory expects configuraton of the following form:  
  /// <code>
  /// &lt;factory type="Slf.Factories.SimpleLoggerFactory, SLF"&gt;
  ///   &lt;factory-data&gt;
  ///     &lt;logger type="Slf.ConsoleLogger, SLF" /&gt;
  ///   &lt;/factory-data&gt;
  /// &lt;/factory&gt;
  /// </code>
  /// Where the type attribute of the logger element indicates the
  /// logger type that this factory returns.
  /// </example>
  public class SingleLoggerFactory : SimpleLoggerFactory, IConfigurableLoggerFactory
  {
    protected ILogger DiagnosticLogger
    {
      get { return LoggerService.GetDiagnosticLogger(GetType().FullName); }
    }

    /// <summary>
    /// Inits the plug-in with configured factory data.
    /// </summary>
    /// <param name="factoryData">Retrieved factory settings.
    /// This parameter is null if no configuration at all
    /// was found.</param>
    public void Init(string factoryData)
    {
      String loggerTypeName;

      try
      {
        // load the factoryData XML
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(factoryData);

        // obtain the logger element and inspect the 'type' attribute
        XmlElement loggerElement = (XmlElement)xmlDoc.GetElementsByTagName("logger")[0];
        XmlAttribute loggerTypeAttribute = loggerElement.Attributes[0];
        loggerTypeName = loggerTypeAttribute.Value;
      }
      catch (Exception e)
      {

        DiagnosticLogger.Error(e, "An exception was thrown while trying to parse the given XML configuration [{0}]",
          factoryData);
        return;
      }

      ILogger logger = ActivatorUtils.Instantiate<ILogger>(loggerTypeName, DiagnosticLogger);
      if (logger != null)
      {
        Logger = logger;
      }
    }
    
  }
}
