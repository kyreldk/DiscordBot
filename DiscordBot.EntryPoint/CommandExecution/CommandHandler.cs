using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using ICommand = DiscordBot.BotCommands.ICommand;

namespace DiscordBot.EntryPoint.CommandExecution
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEnumerable<ICommand> _commands;
        private readonly ILogger<CommandHandler> _logger;

        public CommandHandler(IEnumerable<ICommand> commands, ILogger<CommandHandler> logger)
        {
            _commands = commands;
            _logger = logger;
        }

        public async Task OnMessage(SocketMessage message)
        {
            foreach (var command in _commands)
            {
                try
                {
                    if (!command.CanExecute(message)) continue;

                    await command.Execute(message);
                    break; // every message may only execute one command
                }
                catch (Exception e)
                {
                    _logger.LogError($"Could not execute command. [[{message.Content}]] - {e.Message} - {e.InnerException?.Message}");
                }
            }
        }
    }
}