using System;
using System.Collections.Generic;
using System.Text;
using Slf.Factories;

namespace Slf.EntLibFacade
{
  /// <summary>
  /// An implementation of the <see cref="ILoggerFactory"/>
  /// interface which creates <see cref="ILogger"/> instances
  /// that use the Microsoft Enterprise Library Logging
  /// framework as the underlying logging mechanism.
  /// </summary>
  /// 
  /// <remarks>
  /// <para>This facade logs messages via the
  /// Microsoft Enterprise Library Logging (EntLib)
  /// framework. log levels are mapped as follows:</para>
  /// 
  /// <table>
  /// <tr>
  ///   <th>SLF LogLevel</th>
  ///   <th>EntLib TraceEventType</th>
  /// </tr>
  /// <tr>
  ///   <td>Debug</td>
  ///   <td>Verbose</td>
  /// </tr>
  /// <tr>
  ///   <td>Info</td>
  ///   <td>Informational</td>
  /// </tr>
  /// <tr>
  ///   <td>Warn</td>
  ///   <td>Warning</td>
  /// </tr>
  /// <tr>
  ///   <td>Error</td>
  ///   <td>Error</td>
  /// </tr>
  /// <tr>
  ///   <td>Fatal</td>
  ///   <td>Critical</td>
  /// </tr>
  /// </table>
  /// 
  /// <para>SLF logger names are mapped to EntLib categories, with category names
  /// separated by commas. For example, a logger named
  /// <code>"Integration, Service"</code>, will log to both the <code>Integration</code>
  /// and <code>Service</code> EntLib categories.
  /// </para>
  /// </remarks>
  /// 
  public class EnterpriseLoggerFactory :
    NamedLoggerFactoryBase<EnterpriseLibraryLogger>
  {
    protected override EnterpriseLibraryLogger CreateLogger(string name)
    {
      return new EnterpriseLibraryLogger(name);
    }
  }
}
