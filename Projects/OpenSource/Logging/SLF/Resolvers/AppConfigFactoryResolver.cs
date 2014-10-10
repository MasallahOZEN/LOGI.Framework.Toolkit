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
using System.Configuration;
using Slf.Config;
using Slf.Factories;

namespace Slf.Resolvers
{
  /// <summary>
  /// Resolves factories to be used for logger instantiation based
  /// on settings taken from the application's default configuration
  /// file (<c>App.config</c> or <c>Web.config</c>).
  /// </summary>
  public class AppConfigFactoryResolver : NamedFactoryResolver
  {
    /// <summary>
    /// The name of the SLF configuration section
    /// </summary>
    private const string ConfigSectionName = "slf";

    /// <summary>
    /// A logger used to log problems with the underlying configuration
    /// of loggers and factories. Per default, an <see cref="DebugLogger"/>
    /// is used.
    /// </summary>
    protected ILogger DiagnosticLogger
    {
      get { return LoggerService.GetDiagnosticLogger(GetType().FullName); }
    }


    public AppConfigFactoryResolver()
    {
      Load();
    }


    /// <summary>
    /// Loads the resolver configuration from the applications App.config file.
    /// </summary>
    public virtual void Load()
    {
      SlfConfigurationSection config = ParseConfiguration();
      LoadConfiguration(config);
    }

    /// <summary>
    /// Parses the application configuration to locate and extract the
    /// SLF configuration section
    /// </summary>
    private static SlfConfigurationSection ParseConfiguration()
    {
      return (SlfConfigurationSection) ConfigurationManager.GetSection(ConfigSectionName);
    }


    /// <summary>
    /// Prepares the internal dictionaries and caches for factory
    /// requests, but without already creating the fatory instances.
    /// </summary>
    /// <param name="configuration">An SLF configuration section that
    /// provides configuration settings about factories and loggers.</param>
    protected void LoadConfiguration(SlfConfigurationSection configuration)
    {
      // construct the named factories.
      var factories = new Dictionary<string, ILoggerFactory>();
      if (configuration != null && configuration.Factories != null)
      {
        foreach (FactoryConfigurationElement factoryConfiguration in configuration.Factories)
        {
          ILoggerFactory factory = CreateFactoryInstance(factoryConfiguration);

          if (factories.ContainsKey(factoryConfiguration.Name))
          {
            DiagnosticLogger.Warn("There are duplicate factories with the name [{0}]", factoryConfiguration.Name);
          }
          else
          {
            factories.Add(factoryConfiguration.Name, factory);
          }
        }
      }

      //if there is no default factory, create an implicit entry that
      //resolves to a NullLoggerFactory
      if (!factories.ContainsKey(LoggerService.DefaultLoggerName))
      {
        factories.Add(LoggerService.DefaultLoggerName, NullLoggerFactory.Instance);
      }

      //process logger configurations
      List<LoggerConfigurationElement> loggerConfigurationList = new List<LoggerConfigurationElement>();
      if (configuration != null && configuration.Loggers != null)
      {
        foreach (LoggerConfigurationElement loggerConfiguration in configuration.Loggers)
        {
          loggerConfigurationList.Add(loggerConfiguration);

          //make sure a correct factory name is referenced
          string factoryName = loggerConfiguration.FactoryName;
          if (!factories.ContainsKey(factoryName))
          {
            const string msg = "Declared logger configuration '{0}' refers to undeclared logger factory '{1}'";
            DiagnosticLogger.Error(msg, loggerConfiguration.LoggerName, factoryName);

            // associate with a null logger factory
            base.RegisterFactory(loggerConfiguration.LoggerName, NullLoggerFactory.Instance);
          }
          else
          {
            ILoggerFactory factory = factories[factoryName];
            base.RegisterFactory(loggerConfiguration.LoggerName, factory);
          }

        }
      }

      // if there is no default logger declaration, create one that links to the default factory
      if (loggerConfigurationList.Find(el => el.LoggerName == LoggerService.DefaultLoggerName) == null)
      {
        base.RegisterFactory(LoggerService.DefaultLoggerName, factories[LoggerService.DefaultLoggerName]);
      }

    }

    /// <summary>
    /// Creates a factory based on a given configuration.
    /// If the factory provides invalid information, an error is logged through
    /// the internal logger, and a <see cref="NullLoggerFactory"/> returned.
    /// </summary>
    /// <param name="factoryConfiguration">The configuration that provides type
    /// information for the <see cref="ILoggerFactory"/> that is being created.</param>
    /// <returns>Factory instance.</returns>
    private ILoggerFactory CreateFactoryInstance(FactoryConfigurationElement factoryConfiguration)
    {
      ILoggerFactory factory = ActivatorUtils.Instantiate<ILoggerFactory>(
        factoryConfiguration.Type, DiagnosticLogger);

      //if the factory is configurable, invoke its Init method
      IConfigurableLoggerFactory cf = factory as IConfigurableLoggerFactory;
      if (cf != null)
      {
        cf.Init(factoryConfiguration.FactoryData);
      }

      if (factory == null)
      {
        factory = NullLoggerFactory.Instance;
      }


      return factory;
    }

  }
}