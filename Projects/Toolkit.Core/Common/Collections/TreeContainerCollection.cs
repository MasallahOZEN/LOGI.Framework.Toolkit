using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Common.Collections
{
    [CollectionDataContract]
    public class TreeContainerCollection : BusinessObjectCollection<TreeContainer>
    {        
        [DataMember]
        public string Id { get; set; }
    }
}
