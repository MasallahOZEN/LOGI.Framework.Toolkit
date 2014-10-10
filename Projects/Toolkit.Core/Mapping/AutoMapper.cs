using EmitMapper;

namespace LOGI.Framework.Toolkit.Core.Mapping
{
    public static class AutoMappers
    {
        public static TDestinaitonType Map<TSourceType, TDestinaitonType>(TSourceType sourceItem)
        {
            var result = AutoMapper.Mapper.Map<TSourceType, TDestinaitonType>(sourceItem);

            return result;
        }
    }
}
