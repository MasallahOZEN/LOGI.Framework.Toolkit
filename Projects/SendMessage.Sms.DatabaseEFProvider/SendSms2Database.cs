using System;
using LOGI.Framework.Toolkit.Core.SendMessage.Repository;
using LOGI.Framework.Toolkit.Foundation.Repository;
using LOGI.Framework.Toolkit.Foundation.SendMessage;

namespace LOGI.Framework.Toolkit.Core.SendMessage.Sms.Provider.Database
{
    /// <summary>
    /// SendMessage to Database with EF
    /// </summary>
    public class SendSms2Database : ISendMessage
    {
        public ISendSmsRepository SendMessageRepo { get; set; }

        public SendSms2Database()
        {

        }

        #region Implementation of ISendMessage

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <returns>bool</returns>
        public bool SendMessage<TMessageItem>(TMessageItem messageItem) where TMessageItem : EntityBase
        {
            if (!(messageItem is Model.SMSQueue))
            {
                throw new ArgumentException("TMessageItem -> SMSQueue olmalı");
            }

            if (SendMessageRepo == null)
            {
                throw new ArgumentException("ISendSmsRepository -> null olmamalı");
            }

            if ((messageItem as Model.SMSQueue) == null)
            {
                throw new ArgumentException("TMessageItem -> SMSQueue null olmamalı");
            }

            SendMessageRepo.Add(messageItem as Model.SMSQueue);
            SendMessageRepo.SaveChanges();

            return true;
        }

        #endregion
    }
}
