using System.Threading.Tasks;
using Discord.WebSocket;

using DiscordBot.StaticValues;

namespace DiscordBot.BotCommands.Commands.Hug
{
    public class HugCommand: ICommand
    {
        public bool CanExecute(SocketMessage message)
        {
            var botId = UserIds.BadgerBot;
            return message.Content.Contains($"hugs <@{botId}>");
        }

        public async Task Execute(SocketMessage message)
        {
            await message.Channel.SendMessageAsync("BadgerBot doesn't care. BadgerBot tears out " + message.Author.Mention + "s hair.");
        }
    }
}
