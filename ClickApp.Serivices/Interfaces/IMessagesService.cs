using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IMessagesService
    {
        List<Message> GetAllMessagesWithFriend(string userId, string messageFriendId);
        void CreateMessage(string userId, string messageFriendId, string content);
        bool CheckIfNotSeenMessage(List<Message> friendMessages);
        void UpdateMessage(Message message);
        List<Message> GetAllByFriendId(List<Message> friendMessages, string friendId);
    }
}
