using System.Linq;

namespace LOGI.Framework.Toolkit.Core.Validations
{
    ///<summary>
    /// Validator
    ///</summary>
    public static class Validator
    {
        /// <summary>
        /// Check string argument arrays not null or empties
        /// </summary>
        /// <param name="sourceValues"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpties(params string[] sourceValues)
        {
            return sourceValues.All(strItem => !string.IsNullOrEmpty(strItem));
        }
    }
}
