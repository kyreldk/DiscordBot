using Microsoft.EntityFrameworkCore;

namespace DiscordBot.DataAccess
{
    public interface IBotDbContextFactory
    {
        BotDbContext Create();
    }

    public class BotDbContextFactory : IBotDbContextFactory
    {
        private string _connectionString;
        private DbContextOptionsBuilder<BotDbContext> _optionsBuilder;

        public BotDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
            
            _optionsBuilder = new DbContextOptionsBuilder<BotDbContext>();
            _optionsBuilder.UseSqlServer(_connectionString);

        }

        public BotDbContext Create()
        {
            return new BotDbContext(_optionsBuilder.Options);
        }
    }
}