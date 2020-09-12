using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot.BotCommands
{
    /// <summary>
    /// for ever message that contains the Bot trigger, ever class implementing ICommand will be executed.
    /// First, CanExecute, and if CanExecute returns true, it will follow the Execute method.
    /// CanExecute should run through as quickly as possible.
    ///
    /// Dependency injection on commands should be done over the constructor
    /// </summary>
    public interface ICommand
    {
        bool CanExecute(SocketMessage message);
        Task Execute(SocketMessage message);
    }
}