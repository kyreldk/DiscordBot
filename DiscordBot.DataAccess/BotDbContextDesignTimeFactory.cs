using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DiscordBot.DataAccess
{
    public class BotDbContextDesignTimeFactory: IDesignTimeDbContextFactory<BotDbContext>
    {
        public BotDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BotDbContext>();
            optionsBuilder.UseSqlite("Data Source=discordbot.db");

            return new BotDbContext(optionsBuilder.Options);
        }
    }
}