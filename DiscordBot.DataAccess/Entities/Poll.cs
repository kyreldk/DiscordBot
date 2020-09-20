using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.DataAccess.Entities
{
    [Index(propertyNames: new []{"MessageId"}, IsUnique = true, Name = "Idx_Poll_MessageId")]
    public class Poll
    {
        [Key]
        public int Id { get; set; }
        public ulong MessageId { get; set; }
        public DateTime CreateDate { get; set; }
        
        public List<Pollvote> Pollvotes { get; set; }
    }
}