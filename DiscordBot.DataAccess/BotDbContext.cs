using DiscordBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.DataAccess
{
    public class BotDbContext: DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Pollvote> Pollvotes { get; set; }
    }
}