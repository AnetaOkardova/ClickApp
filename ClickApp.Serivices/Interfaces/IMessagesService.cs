using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IMessagesService
    {
        List<Message> GetAllMessagesWithFilter(string userId, string messageFriendId);
        void CreateMessage(string userId, string messageFriendId, string content);
    }
}
