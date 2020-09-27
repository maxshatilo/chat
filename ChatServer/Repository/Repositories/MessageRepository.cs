namespace ChatServer.Repository.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contexts;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MessageRepository : IMessageRepository
    {
        private readonly MessageDbContext _context;

        public MessageRepository(MessageDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Message>> GetAll()
        {
            var messages = await _context.Messages.AsNoTracking().ToListAsync();
            return messages;
        }

        public async Task<Message> Add(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}
