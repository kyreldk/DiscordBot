using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot.BotCommands.Commands.Ping
{
    public class PingCommand: ICommand
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