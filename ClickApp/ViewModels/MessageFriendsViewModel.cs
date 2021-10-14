using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class MessageFriendsViewModel
    {
        public ApplicationUser MessagesFriend { get; set; }
        public bool LastSeenStatus { get; set; }
    }
}
