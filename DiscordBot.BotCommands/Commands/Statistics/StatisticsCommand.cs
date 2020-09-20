using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordBot.DataAccess;
using DiscordBot.StaticValues;

namespace DiscordBot.BotCommands.Commands.Statistics
{
    public class StatisticsCommand: ICommand
    {
        private readonly IBotDbContextFactory _contextFactory;
        private readonly DiscordSocketClient _discordClient;

        public StatisticsCommand(IBotDbContextFactory contextFactory, DiscordSocketClient discordClient)
        {
            _contextFactory = contextFactory;
            _discordClient = discordClient;
        }

        public bool CanExecute(SocketMessage message)
        {
            var userRoles = ((SocketGuildUser) message.Author).Roles;
            return message.Content.StartsWith("!!stats") && userRoles.Any(e => e.Name == RoleNames.Favoured);
        }

        public async Task Execute(SocketMessage message)
        {
            var dbContext = _contextFactory.Create();

            var messages = dbContext.GetPast24hMessages();

            var byUser = messages
                .GroupBy(m => m.UserName)
                .OrderByDescending(e => e.Count())
                .Take(12)
                .ToList();
            
            var byChannel = messages
                .GroupBy(m => m.ChannelId)
                .OrderByDescending(e => e.Count())
                .Take(6)
                .ToList();
            
            var embedBuilder = new EmbedBuilder();
            
            embedBuilder.Color = Color.Teal;
            embedBuilder.Title = "Statistics of the past 24 hours";
            
            var userSb = new StringBuilder();
            byUser.ForEach(e => userSb.AppendLine($"{e.Key}: {e.Count().ToString()}"));
            
            var channelSb = new StringBuilder();
            byChannel.ForEach(e => channelSb.AppendLine($"<#{e.Key.ToString()}>: {e.Count().ToString()}"));
            
            embedBuilder.AddField(b => b.WithName("Total messages").WithValue(messages.Count().ToString()));
            
            embedBuilder.AddField(b => b.WithName("Top by user").WithValue("Top messaging users in the past 24 hours"));
            byUser.ForEach(bu => embedBuilder.AddField(b => b.WithName(bu.Key).WithValue(bu.Count().ToString()).WithIsInline(true)));
            
            embedBuilder.AddField(b => b.WithName("Top by Channel").WithValue("Top messaging channels in the past 24 hours"));
            byChannel.ForEach(bu => embedBuilder.AddField(b => b.WithName((_discordClient.GetChannel(bu.Key) as ISocketMessageChannel)?.Name ?? "Unknown").WithValue(bu.Count().ToString()).WithIsInline(true)));
            
            await message.Channel.SendMessageAsync(null, false, embedBuilder.Build());
        }
    }
}