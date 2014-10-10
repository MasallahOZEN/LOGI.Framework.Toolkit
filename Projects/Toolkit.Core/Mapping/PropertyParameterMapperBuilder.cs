using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LOGI.Framework.Toolkit.Core.Extensions.ExtString;

namespace LOGI.Framework.Toolkit.Core.Mapping
{
    public sealed class PropertyParameterMapperBuilder<TSourceType>
    {
        private readonly Dictionary<string, string> PropertyParameterMapperCollection;

        public PropertyParameterMapperBuilder()
        {
            PropertyParameterMapperCollection = new Dictionary<string, string>();
        }

        public PropertyParameterMapperBuilder<TSourceType> AddKey<TProp>(Expression<Func<TSourceType, TProp>> expression, string parameterName)
        {

            var propName = LOGI.Framework.Toolkit.Core.Reflection.PropertyUtil.GetName(expression);

            #region Validation
            if (parameterName.IsNullOrEmpty())
            {
                throw new ArgumentNullException("parameterName", "Parameter Name zorunlu !!!");
            }
            #endregion

            PropertyParameterMapperCollection.Add(propName, parameterName);

            return this;
        }

        public string GetKeyValue<TProp>(Expression<Func<TSourceType, TProp>> expression)
        {
            var returnVal =string.Empty;
            var propName = LOGI.Framework.Toolkit.Core.Reflection.PropertyUtil.GetName(expression);

            var keyExist = PropertyParameterMapperCollection.ContainsKey(propName);
            if (keyExist)
            {
                returnVal = PropertyParameterMapperCollection[propName];
            }

            return returnVal;
        }
    }
}
