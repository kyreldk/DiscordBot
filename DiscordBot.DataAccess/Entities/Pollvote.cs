using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.DataAccess.Entities
{
    [Index(propertyNames: new []{"UserId"}, IsUnique = false, Name = "Idx_Pollvote_UserId")]
    public class Pollvote
    {
        [Key]
        public int Id { get; set; }
        public ulong UserId { get; set; }
        public int VoteOption { get; set; }
        public DateTime CreateDate { get; set; }
        
        public Poll Poll { get; set; }
    }
}