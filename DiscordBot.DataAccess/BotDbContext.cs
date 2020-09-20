using System;
using System.Collections.Generic;
using DiscordBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DiscordBot.DataAccess
{
    public class BotDbContext: DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Pollvote> Pollvotes { get; set; }
        public DbSet<Message> Messages { get; set; }

        public List<Message> GetPast24hMessages()
        {
            var yesterday = DateTime.Now.Subtract(TimeSpan.FromHours(24));
            return Messages.Where(e => e.CreateDate >= yesterday).ToList();
        }
    }
}