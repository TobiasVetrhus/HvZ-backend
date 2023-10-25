using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Conversations
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Conversation entities.
    /// </summary>
    public interface IConversationService : ICrudService<Conversation, int>
    {
    }
}
