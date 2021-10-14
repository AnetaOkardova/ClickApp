using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class MessagesViewModel
    {
        public List<MessageFriendsViewModel> MessageFriends { get; set; }
        public List<Message> Messages { get; set; }
        public CreateMessageViewModel NewMessage { get; set; }

    }
}
