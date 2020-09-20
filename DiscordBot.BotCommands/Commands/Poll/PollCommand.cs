using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace DiscordBot.BotCommands.Commands.Poll
{
    public class PollCommand: ICommand
    {
        private readonly IPollRepository _pollRepository;
        
        private static List<string> numberEmoji = new List<string>()
        {
            "1️⃣",
            "2️⃣",
            "3️⃣",
            "4️⃣",
            "5️⃣",
            "6️⃣",
            "7️⃣",
            "8️⃣",
            "9️⃣",
        };

        public PollCommand(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public bool CanExecute(SocketMessage message)
        {
            return message.Content.StartsWith("!!poll");
        }

        public async Task Execute(SocketMessage message)
        {
            var messageContent = message.Content.Replace("!!poll", "");
            
            var pollOptions = CreatePollOptions(messageContent);
            var pollQuestion = GetPollQuestion(messageContent);

            var embed = CreatePollEmbed(message.Author.Username, pollOptions, pollQuestion);

            var createdPoll = await message.Channel.SendMessageAsync(null, false, embed);

            await AddPollReactions(createdPoll, pollOptions);
            _pollRepository.RegisterMessageAsPoll(createdPoll.Id);

            await message.DeleteAsync(); // delete poll command message
        }

        private static async Task AddPollReactions(RestUserMessage message, List<string> pollOptions)
        {
            for (var i = 0; i < pollOptions.Count; ++i)
            {
                await message.AddReactionAsync(new Emoji(numberEmoji[i].Replace(":", "")));
            }
        }

        private static Embed CreatePollEmbed(string author, List<string> pollOptions, string pollQuestion)
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.Color = Color.Green;

            embedBuilder.AddField(builder => builder.WithName($"Poll from {author}").WithValue(pollQuestion));
            
            var optionsSb = new StringBuilder();
            for (var i = 0; i < pollOptions.Count; ++i)
            {
                optionsSb.AppendLine($"{numberEmoji[i]} = {pollOptions[i]}");
            }
            
            embedBuilder.AddField(builder =>
            {
                builder.WithName("Options").WithValue(optionsSb);
            });

            return embedBuilder.Build();
        }
        
        private static List<string> CreatePollOptions(string message)
        {
            var pollOptions = new List<string>();

            if (message.Contains("|"))
            {
                pollOptions = message.Split("|").Select(o => o.Trim()).ToList();
                pollOptions.RemoveAt(0);
            }
            else
            {
                pollOptions.Add("yes");
                pollOptions.Add("no");
            }

            return pollOptions;
        }

        private static string GetPollQuestion(string message)
        {
            return message.Contains("|") ? message.Split("|").First().Trim() : message.Trim();
        }
    }
}

