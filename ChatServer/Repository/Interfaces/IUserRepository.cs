namespace ChatServer.Repository.Interfaces
{
    using System.Threading.Tasks;
    using Domain.Entities;

    public interface IUserRepository
    {
        Task<User> GetByUserName(string userName);
    }
}
