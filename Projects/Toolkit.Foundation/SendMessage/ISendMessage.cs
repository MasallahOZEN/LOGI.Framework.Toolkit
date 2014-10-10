using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGI.Framework.Toolkit.Foundation.Repository;

namespace LOGI.Framework.Toolkit.Foundation.SendMessage
{
    /// <summary>
    /// SMS,Email vb mesajlarının gönderilmesi için gereken şema
    /// </summary>
    public interface ISendMessage
    {
        /// <summary>
        /// SendMessage
        /// </summary>
        /// <returns>bool</returns>
        bool SendMessage<TMessageItem>(TMessageItem messageItem) where TMessageItem:EntityBase;
    }
}
