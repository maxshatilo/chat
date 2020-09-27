namespace ChatServer.Infrastructure.Helpers.Identity.Interfaces
{
    using System.Threading.Tasks;

    public interface IJWTTokenProvider
    {
        Task<string> GetJWTToken(string UserName);
    }
}
