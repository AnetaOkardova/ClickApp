using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string FriendsId { get; set; }
        [Required]
        public bool FriendShipStatus { get; set; }
        [Required]
        public DateTime FrienshipCreated { get; set; }
        public DateTime FrienshipEnded { get; set; }
    }
}
