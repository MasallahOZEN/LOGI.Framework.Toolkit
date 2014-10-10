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
using System.Globalization;
using Slf.Factories;

namespace Slf.Resolvers
{
  /// <summary>
  /// A resolver that is able to map factories to logger names, and
  /// resolve them in a hierarchical manner.
  /// </summary>  
  public class NamedFactoryResolver : IFactoryResolver
  {
    /// <summary>
    /// A list of named factories sorted in order of decreasing
    /// specificity.
    /// </summary>
    private readonly List<NamedFactory> _factoryCache;

    /// <summary>
    /// An object which is used for locking
    /// </summary>
    private object lockObj  = new object();

    /// <summary>
    /// Creates a new instance of the resolver.
    /// </summary>
    public NamedFactoryResolver()
    {
      lock (lockObj)
      {
        _factoryCache = new List<NamedFactory>();
      }
    }


    /// <summary>
    /// Checks for the occurrence of a given factory
    /// in the internal cache.
    /// </summary>
    /// <param name="name">The logger name that was
    /// associated with the factory.</param>
    /// <returns></returns>
    public bool ContainsFactory(string name)
    {
      Ensure.ArgumentNotNull(name, "name");
      name = name.ToLower(CultureInfo.InvariantCulture);

      lock (lockObj)
      {
        return _factoryCache.Find(nf => nf.Name.ToLower(CultureInfo.InvariantCulture) == name) != null;
      }
    }


    /// <summary>
    /// Adds a factory that resolves to the given name.
    /// </summary>
    /// <param name="name">The name that was used to register a given
    /// factory. Name comparison is case insensitive.</param>
    /// <param name="factory">The factory that is associated with the name.</param>
    /// <exception cref="ArgumentException">If a factory with the same name
    /// was already registered.</exception>
    /// <exception cref="ArgumentNullException">If one of the parameters
    /// is a null reference.</exception>
    /// <exc
    public void RegisterFactory(string name, ILoggerFactory factory)
    {
      Ensure.ArgumentNotNull(name, "name");
      Ensure.ArgumentNotNull(factory, "factory");
      
      lock (lockObj)
      {
        if (ContainsFactory(name))
        {
          string msg = String.Format("There's already a factory registered by the name '{0}'.", name);
          throw new ArgumentException(msg);
        }

        //register and sort the cache in order of decreasing specificity
        _factoryCache.Add(new NamedFactory(name, factory));
        _factoryCache.Sort((f1, f2) => f2.Name.Length.CompareTo(f1.Name.Length));
      }
    }



    /// <summary>
    /// Removes a previously registered factory from the resolver's internal cache.
    /// </summary>
    /// <param name="name">The name that was used to register the factory. Comparison
    /// is case insensitive.</param>
    /// <returns>True if a matching factory was found and removed.</returns>
    public bool DeregisterFactory(string name)
    {
      Ensure.ArgumentNotNull(name, "name");
      name = name.ToLower(CultureInfo.InvariantCulture);

      lock (lockObj)
      {
        // locate the first factory which matches the named logger
        NamedFactory namedFactory = _factoryCache.Find(factory => name == factory.Name.ToLower(CultureInfo.InvariantCulture));
        if (namedFactory != null)
        {
          _factoryCache.Remove(namedFactory);
          return true;
        }
      }

      return false;
    }



    /// <summary>
    /// Determines a factory which can create an
    /// <see cref="ILogger"/> instance based on a
    /// request for a named logger.
    /// </summary>
    /// <param name="loggerName">The logger name.</param>
    /// <returns>A factory which in turn is responsible for creating
    /// a given <see cref="ILogger"/> implementation.</returns>
    public ILoggerFactory GetFactory(string loggerName)
    {
      Ensure.ArgumentNotNull(loggerName,"loggerName");

      loggerName = loggerName.ToLower(CultureInfo.InvariantCulture);
      ILoggerFactory resolvedFactory;

      lock (lockObj)
      {
        if (_factoryCache.Count == 0)
        {
          return NullLoggerFactory.Instance;
        }

        // locate the first factory which matches the named logger
        NamedFactory namedFactory = _factoryCache.Find(
            factory => loggerName.StartsWith(factory.Name.ToLower(CultureInfo.InvariantCulture)));


        if (namedFactory != null)
        {
          resolvedFactory = namedFactory.Factory;
        }
        else
        {
          // if there are no matches, use the default logger, i.e
          // a logger with an empty name

          // get the least specific factory
          namedFactory = _factoryCache[_factoryCache.Count - 1];

          if (namedFactory.Name == LoggerService.DefaultLoggerName)
          {
            // this is the default logger. so return the factory
            resolvedFactory = namedFactory.Factory;
          }
          else
          {
            // the least specific factory is not a default factory, so 
            // we have no other option than to return a null logger factory
            resolvedFactory = NullLoggerFactory.Instance;
          }
        }
      }

      return resolvedFactory;
    }


    /// <summary>
    /// Associates a factory with a name which is used
    /// to determine whether a request for a named logger resolves
    /// to the associated factory.
    /// </summary>
    public class NamedFactory
    {
      public NamedFactory(string name, ILoggerFactory factory)
      {
        Factory = factory;
        Name = name;
      }

      public ILoggerFactory Factory { get; private set; }

      public string Name { get; private set; }
    }
  }
}