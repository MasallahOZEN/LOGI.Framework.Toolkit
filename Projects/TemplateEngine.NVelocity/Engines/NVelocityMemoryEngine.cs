using System.Collections;
using System.IO;
using NVelocity.Exception;
using LOGI.Framework.Toolkit.Core.TemplateEngines.NVelocity.BaseClasses;
using LOGI.Framework.Toolkit.Foundation.TemplateEngines;

namespace LOGI.Framework.Toolkit.Core.TemplateEngines.NVelocity.Engines
{
	/// <summary>
	/// Summary description for NVelocityMemoryEngine.
	/// </summary>
    public sealed class NVelocityMemoryEngine : NVelocityEngineBase, ITemplateEngine
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NVelocityMemoryEngine"/> class.
		/// </summary>
		/// <param name="cacheTamplate">if set to <c>true</c> [cache tamplate].</param>
		internal NVelocityMemoryEngine(bool cacheTamplate) : base(cacheTamplate)
		{
			this.Init();
		}

		/// <summary>
		/// Processes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="template">The template.</param>
		/// <returns></returns>
		public string Process(IDictionary context, string template)
		{
			var writer = new StringWriter();

			try
			{
				this.Evaluate(CreateContext(context), writer, "mystring", template);
			}
			catch (ParseErrorException pe)
			{	
				return pe.Message;
			} 
			catch (MethodInvocationException mi)
			{
				return mi.Message;
			}

			return writer.ToString();
		}

		/// <summary>
		/// Processes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="writer">The writer.</param>
		/// <param name="template">The template.</param>
		public void Process(IDictionary context, TextWriter writer, string template)
		{
			try
			{
				this.Evaluate(CreateContext(context), writer, "mystring", template);
			}
			catch (ParseErrorException pe)
			{	
				writer.Write(pe.Message);
			} 
			catch (MethodInvocationException mi)
			{
				writer.Write(mi.Message);
			}
		}
	}
}
