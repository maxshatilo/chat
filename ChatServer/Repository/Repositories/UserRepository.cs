namespace ChatServer.Repository.Repositories
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Contexts;
    using Domain.Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetByUserName(string userName)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(p => p.UserName == userName);
            return _mapper.Map<User>(dbUser);
        }
    }
}
