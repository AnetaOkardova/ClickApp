using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClickApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ProfilePhotoURL { get; set; }

        public List<UserSkill> Skills { get; set; }
        public List<UserInterest> Interests { get; set; }
        public List<Offer> Offers { get; set; }
        public List<CarpoolOffer> CarpoolOffers { get; set; }
        
        public List<FriendshipRequest>? FriendshipRequests { get; set; }
        public List<Friendship>? Friendships { get; set; }
        public List<JournalEntry> JournalEntries { get; set; }
        public List<Note> Notes { get; set; }
        //public List<string> JournalThemes { get; set; } 
    }
}
