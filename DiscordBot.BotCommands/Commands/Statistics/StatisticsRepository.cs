using DiscordBot.DataAccess;

namespace DiscordBot.BotCommands.Commands.Statistics
{
    public class StatisticsRepository
    {
        private readonly IBotDbContextFactory _dbContextFactory;

        public StatisticsRepository(IBotDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            
        }

        public void Statistics()
        {
            var context = _dbContextFactory.Create();
        }
    }
}