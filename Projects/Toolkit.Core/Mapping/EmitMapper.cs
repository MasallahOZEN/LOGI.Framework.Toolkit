using EmitMapper;

namespace LOGI.Framework.Toolkit.Core.Mapping
{
    public static class EmitMapper
    {
        public static TDestinaitonType Map<TSourceType, TDestinaitonType>(TSourceType sourceItem)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TSourceType, TDestinaitonType>();

            var result = mapper.Map(sourceItem);

            return result;
        }
    }
}
