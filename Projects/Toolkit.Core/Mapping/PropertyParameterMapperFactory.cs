using System.Collections.Generic;
using LOGI.Framework.Toolkit.Core.Reflection;

namespace LOGI.Framework.Toolkit.Core.Mapping
{
    public class PropertyParameterMapperFactory 
    {
        // Gets the single instance of SingleInstanceClass.
        public static PropertyParameterMapperFactory Instance
        {
            get { return SingletonBase<PropertyParameterMapperFactory>.Instance; }
        }

        private readonly Dictionary<object , dynamic > PropertyParameterMapperBaseTypeCollection;

        public PropertyParameterMapperFactory()
        {
            PropertyParameterMapperBaseTypeCollection = new Dictionary<object, object>();

            
        }

        public PropertyParameterMapperBuilder<TSourceType> Add<TSourceType>()
        {
            var propertyParameterMapperBuilder = new PropertyParameterMapperBuilder<TSourceType>();
            var mapForType = typeof (TSourceType);

            if (PropertyParameterMapperBaseTypeCollection.ContainsKey(mapForType))
            {
                propertyParameterMapperBuilder = PropertyParameterMapperBaseTypeCollection[mapForType];
            }
            else
            {
                PropertyParameterMapperBaseTypeCollection.Add(mapForType, propertyParameterMapperBuilder);
            }

            return propertyParameterMapperBuilder;
        }

        public PropertyParameterMapperBuilder<TSourceType> Get<TSourceType>()
        {
            PropertyParameterMapperBuilder<TSourceType> propertyParameterMapperBuilder = null;
            var mapForType = typeof(TSourceType);

            if (PropertyParameterMapperBaseTypeCollection.ContainsKey(mapForType))
            {
                propertyParameterMapperBuilder = PropertyParameterMapperBaseTypeCollection[mapForType];
            }
            
            return propertyParameterMapperBuilder;
        }
    }
}
