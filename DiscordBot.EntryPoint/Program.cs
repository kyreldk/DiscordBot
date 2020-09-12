using Microsoft.Extensions.Hosting;

namespace DiscordBot.EntryPoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    Initialize.InitializeDiscordClient(hostContext, services);

                    Initialize.RegisterServices(hostContext, services);
                    Initialize.RegisterWorker(hostContext, services);
                });
    }
}
