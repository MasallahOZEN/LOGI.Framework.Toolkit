using System.Collections.Generic;

namespace LOGI.Framework.Toolkit.Foundation.Repository
{
    public class ResultData<T>
    {
        public int Count { get; set; }
        public List<T> Data { get; set; }
    }
}
