using System.Collections;
using LOGI.Framework.Toolkit.Core.TemplateEngines.NVelocity.Engines;
using LOGI.Framework.Toolkit.Core.TemplateEngines.NVelocity.Engines;
using LOGI.Framework.Toolkit.Foundation.TemplateEngines;

namespace LOGI.Framework.Toolkit.Core.TemplateEngines.NVelocity
{
	/// <summary>
	/// Summary description for NVelocityEngineFactory.
	/// </summary>
	public class NVelocityEngineFactory
	{
		/// <summary>
		/// Creates a new instance of NVelocityFileEngine class.
		/// </summary>
		/// <param name="templateDirectory">The template directory.</param>
		/// <param name="cacheTemplate">if set to <c>true</c> [cache template].</param>
		/// <returns></returns>
        public static ITemplateEngine CreateNVelocityFileEngine(string templateDirectory, bool cacheTemplate)
		{
			return new NVelocityFileEngine(templateDirectory, cacheTemplate);
		}

		/// <summary>
		/// Creates a new instance of NVelocityAssemblyEngine class.
		/// </summary>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <param name="cacheTemplate">if set to <c>true</c> [cache template].</param>
		/// <returns></returns>
        public static ITemplateEngine CreateNVelocityAssemblyEngine(string assemblyName, bool cacheTemplate)
		{
			return new NVelocityAssemblyEngine(assemblyName, cacheTemplate);
		}

		/// <summary>
		/// Creates a new instance of NVelocityMemoryEngine class.
		/// </summary>
		/// <param name="cacheTemplate">if set to <c>true</c> [cache template].</param>
		/// <returns></returns>
        public static ITemplateEngine CreateNVelocityMemoryEngine(bool cacheTemplate)
		{
			return new NVelocityMemoryEngine(cacheTemplate);
		}

        
	}
}
