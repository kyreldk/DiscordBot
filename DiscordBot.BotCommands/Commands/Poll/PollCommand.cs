using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot.BotCommands.Commands.Poll
{
    public class PollCommand: ICommand
    {
        public bool CanExecute(SocketMessage message)
        {
            return message.Content.StartsWith("*poll");
        }

        public Task Execute(SocketMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}