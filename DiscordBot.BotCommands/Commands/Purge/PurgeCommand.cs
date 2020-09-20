using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordBot.StaticValues;

namespace DiscordBot.BotCommands.Commands.Purge
{
    public class PurgeCommand: ICommand
    {
        public bool CanExecute(SocketMessage message)
        {
            if (!message.Content.StartsWith("!!purge")) return false;
            if (!int.TryParse(message.Content.Replace("!!purge ", "").Trim(), out _)) return false;

            var userRoles = ((SocketGuildUser) message.Author).Roles;

            return message.Author.Id == UserIds.Kyrel ||
                   userRoles.Any(sr => sr.Name == RoleNames.Caretaker);
        }

        public async Task Execute(SocketMessage message)
        {
            var amount = int.Parse(message.Content.Replace("!!purge ", "").Trim());

            if (amount > 50)
            {
                await message.Channel.SendMessageAsync("Can not purge more than 50 messages");
                return;
            }
            
            var messages = await message.Channel.GetMessagesAsync((int) amount + 1).FlattenAsync();

            await ((ITextChannel) message.Channel).DeleteMessagesAsync(messages);
        }
    }
}