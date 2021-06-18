using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class FriendshipRequest
    {
        public FriendshipRequest()
        {
            RequestSent = true;
            RequestConfirmed = false;
            ActiveRequest = true;
        }
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string RequestedUserId { get; set; }
        [Required]
        public bool RequestSent { get; set; }
        [Required]
        public DateTime RequestCreated { get; set; }
        public bool RequestConfirmed { get; set; } = false;
        public DateTime? DateRequestConfirmed { get; set; }
        public bool ActiveRequest { get; set; }

    }
}
