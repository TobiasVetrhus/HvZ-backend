using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Messages
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Message entities.
    /// </summary>
    public interface IMessageService : ICrudService<Message, int>
    {
    }
}
