using Discord;
using Discord.WebSocket;
using DiscordBot.BotCommands;
using DiscordBot.BotCommands.Commands;
using DiscordBot.EntryPoint.CommandExecution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiscordBot.EntryPoint
{
    public class Initialize
    {
        public static void InitializeDiscordClient(HostBuilderContext hostContext, IServiceCollection services)
        {
            var discordClient = new DiscordSocketClient();
            
            discordClient.LoginAsync(TokenType.Bot, hostContext.Configuration["DiscordApiKey"]).GetAwaiter().GetResult();
            discordClient.StartAsync().GetAwaiter().GetResult();
            
            services.AddSingleton(discordClient);

        }
        public static void RegisterServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<ICommandHandler, CommandHandler>();
            
            services.AddSingleton<ICommand, TestCommand>();
            services.AddSingleton<ICommand, PurgeCommand>();
        }

        public static void RegisterWorker(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddHostedService<BotWorker>();
        }
    }
}