using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordBot.EntryPoint.CommandExecution;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordBot.EntryPoint
{
    public class BotWorker : BackgroundService
    {
        private readonly ILogger<BotWorker> _logger;
        private readonly DiscordSocketClient _discordClient;
        private readonly ICommandHandler _commandHandler;

        public BotWorker(ILogger<BotWorker> logger, DiscordSocketClient discordClient, ICommandHandler commandHandler)
        {
            _logger = logger;
            _discordClient = discordClient;
            _commandHandler = commandHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _discordClient.MessageReceived += _commandHandler.OnMessage;

                _discordClient.Ready += () =>
                {
                    try
                    {
                        var channel =
                            _discordClient.GetGuild(685492295386398780).GetChannel(757206506566320128) as
                                IMessageChannel;
                        channel.SendMessageAsync($"Successful redeploy on Azure {DateTime.Now.ToLongTimeString()}")
                            .GetAwaiter().GetResult();
                    }
                    catch (Exception)
                    {

                    }

                    return null;
                };
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                
                await Task.Delay(-1, stoppingToken);
            }
        }
    }
}
