using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot.BotCommands.Commands
{
    public class TestCommand: ICommand
    {
        public bool CanExecute(SocketMessage message)
        {
            return message.Content.StartsWith("*ping");
        }

        public async Task Execute(SocketMessage message)
        {
            await message.Channel.SendMessageAsync("pong");
        }
    }
}