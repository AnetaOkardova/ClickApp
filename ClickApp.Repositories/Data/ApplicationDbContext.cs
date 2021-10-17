using ClickApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GeneralField> GeneralFields { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<CarpoolOffer> CarpoolOffers { get; set; }
        public DbSet<CarpoolPassengerAcceptance> CarpoolPassengerAcceptances { get; set; }
        public DbSet<CarpoolPassengerRequest> CarpoolPassengerRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<JournalEntry> JournalEntrys { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<JournalTheme> JournalThemes { get; set; }
    }
}
