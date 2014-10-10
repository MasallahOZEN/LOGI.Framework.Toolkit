using System;
using LOGI.Framework.Toolkit.Core.SendMessage.Model;
using LOGI.Framework.Toolkit.Core.SendMessage.Repository;
using LOGI.Framework.Toolkit.Foundation.Repository;
using LOGI.Framework.Toolkit.Foundation.SendMessage;

namespace LOGI.Framework.Toolkit.Core.SendMessage.Email.Provider.Database
{
    /// <summary>
    /// SendMessage to Database with EF
    /// </summary>
    public class SendMessage2Database : ISendMessage
    {
        public ISendMessageEmailRepository SendMessageRepo { get; set; }

        public SendMessage2Database()
        {
            
        }

        #region Implementation of ISendMessage

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <returns>bool</returns>
        public bool SendMessage<TMessageItem>(TMessageItem messageItem) where TMessageItem : EntityBase
        {
            if (!(messageItem is EmailQueue))
            {
                throw new ArgumentException("TMessageItem -> EmailQueue olmalı");
            }

            if (SendMessageRepo == null)
            {
                throw new ArgumentException("ISendMessageRepository -> null olmamalı");
            }

            if ((messageItem as EmailQueue)==null)
            {
                throw new ArgumentException("TMessageItem -> EmailQueue null olmamalı");
            }

            SendMessageRepo.Add(messageItem as EmailQueue);
            SendMessageRepo.SaveChanges();

            return true;
        }

        #endregion
    }
}
