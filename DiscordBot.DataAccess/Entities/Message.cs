using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DiscordBot.DataAccess.Entities
{
    [Index(propertyNames: new []{"ChannelId"}, IsUnique = false, Name = "Idx_Message_ChannelId")]
    [Index(propertyNames: new []{"CreateDate"}, IsUnique = false, Name = "Idx_Message_CreateDate")]
    [Index(propertyNames: new []{"UserId"}, IsUnique = false, Name = "Idx_Message_UserId")]
    public class Message
    {
        [Key]
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong ChannelId { get; set; }
        public int CharCount { get; set; }
        
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}