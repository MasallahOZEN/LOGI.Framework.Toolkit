using LOGI.Framework.Toolkit.Core.SendMessage.Repository;

namespace LOGI.Framework.Toolkit.Core.SendMessage.Email.Provider.Database
{
    ///<summary>
    ///</summary>
    public class SendMessageRepository : Email.Provider.Database.GenericRepository<Model.EmailQueue>,ISendMessageEmailRepository
    {
    }
}
