using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordBot.DataAccess;
using DiscordBot.DataAccess.Entities;
using Microsoft.Extensions.Logging;

namespace DiscordBot.BotCommands.Commands.Statistics
{
    public class StatisticsGeneratorCommand: ICommand
    {
        private readonly IBotDbContextFactory _contextFactory;
        private readonly ILogger<StatisticsGeneratorCommand> _logger;

        public StatisticsGeneratorCommand(IBotDbContextFactory contextFactory, ILogger<StatisticsGeneratorCommand> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public bool CanExecute(SocketMessage message)
        {
            return !message.Author.IsBot;
        }

        public async Task Execute(SocketMessage message)
        {
            var dbContext = _contextFactory.Create();
            
            var dbMessage = new Message
            {
                Id = Convert.ToUInt64(DateTime.Now.Ticks),
                ChannelId = message.Channel.Id,
                CharCount = message.Content.Length,
                UserId = message.Author.Id,
                UserName = message.Author.Username,
                CreateDate = DateTime.Now
            };

            dbContext.Add(dbMessage);
            await dbContext.SaveChangesAsync();
            
            _logger.LogInformation("Saved message");
            
            await dbContext.DisposeAsync();
        }
    }
}