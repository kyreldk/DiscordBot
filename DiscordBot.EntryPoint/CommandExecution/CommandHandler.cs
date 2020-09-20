using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using ICommand = DiscordBot.BotCommands.ICommand;

namespace DiscordBot.EntryPoint.CommandExecution
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEnumerable<ICommand> _commands;
        private readonly ILogger<CommandHandler> _logger;
        private readonly DiscordSocketClient _discordClient;

        public CommandHandler(IEnumerable<ICommand> commands, ILogger<CommandHandler> logger, DiscordSocketClient discordClient)
        {
            _commands = commands;
            _logger = logger;
            _discordClient = discordClient;
        }

        public async Task OnMessage(SocketMessage message)
        {
            if (message.Author.IsBot)
            {
                return;
            }
            
            if (message.Channel is IPrivateChannel)
            {
                return;
            }
            
            foreach (var command in _commands)
            {
                try
                {
                    if (!command.CanExecute(message)) continue;

                    await command.Execute(message);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Could not execute command. [[{message.Content}]] - {e.Message} - {e.InnerException?.Message}");

                    try
                    {
                        var channel = _discordClient.GetGuild(685492295386398780).GetChannel(757206506566320128) as
                            IMessageChannel;
                        
                        channel?.SendMessageAsync($"Could not execute command. [[{message.Content}]] - {e.Message} - {e.InnerException?.Message}")
                            .GetAwaiter().GetResult();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
            }
        }
    }
}