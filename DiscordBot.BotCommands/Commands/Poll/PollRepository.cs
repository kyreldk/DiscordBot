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
        private readonly BotDbContext _dbContext;

        public PollRepository(BotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RegisterMessageAsPoll(ulong messageId)
        {
            var poll = new DataAccess.Entities.Poll
            {
                MessageId = messageId,
                CreateDate = DateTime.Now,
            };

            _dbContext.Add(poll);
            _dbContext.SaveChanges();
        }

        public bool IsMessagePoll(ulong messageId)
        {
            return _dbContext.Polls.FirstOrDefault(e => e.MessageId == messageId) != null;
        }
    }
}