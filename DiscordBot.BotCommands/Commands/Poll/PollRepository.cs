using System;
using System.Linq;
using DiscordBot.DataAccess;

namespace DiscordBot.BotCommands.Commands.Poll
{
    public interface IPollRepository
    {
        void RegisterMessageAsPoll(ulong messageId);
    }

    public class PollRepository : IPollRepository
    {
        private readonly IBotDbContextFactory _dbContextFactory;

        public PollRepository(IBotDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void RegisterMessageAsPoll(ulong messageId)
        {
            using var dbContext = _dbContextFactory.Create();
            var poll = new DataAccess.Entities.Poll
            {
                MessageId = messageId,
                CreateDate = DateTime.Now,
            };

            dbContext.Add(poll);
            dbContext.SaveChanges();
        }

        public bool IsMessagePoll(ulong messageId)
        {
            using var dbContext = _dbContextFactory.Create();
            return dbContext.Polls.FirstOrDefault(e => e.MessageId == messageId) != null;
        }
    }
}