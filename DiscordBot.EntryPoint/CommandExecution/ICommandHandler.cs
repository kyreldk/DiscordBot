using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot.EntryPoint.CommandExecution
{
    public interface ICommandHandler
    {
        Task OnMessage(SocketMessage message);
    }
}