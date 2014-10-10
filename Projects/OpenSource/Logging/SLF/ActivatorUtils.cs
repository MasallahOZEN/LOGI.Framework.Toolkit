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

namespace Slf
{
  /// <summary>
  /// Provides helper methods that supplement the 
  /// <see cref="System.Activator"/>.
  /// </summary>
  internal static class ActivatorUtils
  {
    /// <summary>
    /// Instantiates an object of the given type.
    /// </summary>
    /// <typeparam name="T">The type being instantiated</typeparam>
    /// <param name="typeName">The full type name</param>
    /// <param name="logger">A logger which is used to log any
    /// problems which occur in the instantiation process.</param>
    /// <returns></returns>
    internal static T Instantiate<T>(string typeName, ILogger logger)
    {
      Type typeToInstantiate = Type.GetType(typeName);

      // check that the type can be located
      if (typeToInstantiate == null)
      {
        logger.Error("The item has a type which cannot be located [type={0}].",
            typeName);
        return default(T);
      }

      // create instance of the given type
      object instantiatedInstance;
      try
      {
        instantiatedInstance = Activator.CreateInstance(typeToInstantiate);
      }
      catch (Exception e)
      {
        logger.Error(e, "The item [type={0}], cannot be instantiated",
                           typeToInstantiate);
        return default(T);
      }

      // make sure we do infact have an item of the correct type
      T instantiatedType = default(T);
      try
      {
        instantiatedType = (T)instantiatedInstance;
      }
      catch (InvalidCastException)
      {
        logger.Error("The item [type={0}], is not of type {1}.",
                           typeToInstantiate, typeof(T));
      }

      return instantiatedType;
    }
  }
}
