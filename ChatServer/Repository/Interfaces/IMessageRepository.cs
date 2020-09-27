namespace ChatServer.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IMessageRepository
    {
        Task<IList<Message>> GetAll();

        Task<Message> Add(Message message);
    }
}
