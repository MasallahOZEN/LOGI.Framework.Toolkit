using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Slf.EntLibFacade.Diagnostics;
using SysEnvironment = System.Environment;

namespace Slf.EntLibFacade.Diagnostics
{
    public class VerboseTraceFormatterData : TextFormatterData
    {
        private const string includeAppDomain = "includeAppDomain";
        private const string includeEnvironment = "includeEnvironment";
        private const string includeProcess = "includeProcess";
        private const string includeThread = "includeThread";
        private const string includeType = "includeType";

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFormatterData"/> class with default values.
        /// </summary>
        public VerboseTraceFormatterData()
        {
            Type = typeof(VerboseTraceFormatter);
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="TextFormatterData"/> class with a template.
        /// </summary>
        /// <param name="templateData">
        /// Template containing tokens to replace.
        /// </param>
        public VerboseTraceFormatterData(string templateData)
            : this("unnamed", templateData)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="TextFormatterData"/> class with a name and template.
        /// </summary>
        /// <param name="name">
        /// The name of the formatter.
        /// </param>
        /// <param name="templateData">
        /// Template containing tokens to replace.
        /// </param>
        public VerboseTraceFormatterData(string name, string templateData)
            : this(name, typeof(VerboseTraceFormatter), templateData)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="TextFormatterData"/> class with a name and template.
        /// </summary>
        /// <param name="name">
        /// The name of the formatter.
        /// </param>
        /// <param name="formatterType">
        /// The type of the formatter.
        /// </param>
        /// <param name="templateData">
        /// Template containing tokens to replace.
        /// </param>
        private VerboseTraceFormatterData(string name, Type formatterType, string templateData)
            : base(name)
        {
            this.Template = templateData;
            this.Type = formatterType;
        }

        [ConfigurationProperty(includeAppDomain, IsRequired = true, DefaultValue = true)]
        public bool IncludeAppDomain { get; set; }

        [ConfigurationProperty(includeEnvironment, IsRequired = true, DefaultValue = true)]
        public bool IncludeEnvironment { get; set; }

        [ConfigurationProperty(includeProcess, IsRequired = true, DefaultValue = true)]
        public bool IncludeProcess { get; set; }

        [ConfigurationProperty(includeThread, IsRequired = true, DefaultValue = true)]
        public bool IncludeThread { get; set; }

        [ConfigurationProperty(includeType, IsRequired = true, DefaultValue = true)]
        public bool IncludeType { get; set; }
    }
}
