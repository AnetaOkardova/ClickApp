using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _messagesRepository;

        public MessagesService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }


        public void CreateMessage(string userId, string messageFriendId, string content)
        {
            var message = new Message();
            message.UserFromId = userId;
            message.UserToId = messageFriendId;
            message.Content = content;
            message.DateCreated = DateTime.Now;
            message.SeenStatus = false;

            _messagesRepository.Create(message);
        }
        public void UpdateMessage(Message message)
        {
            _messagesRepository.Update(message);
        }

        public List<Message> GetAllMessagesWithFriend(string userId, string messageFriendId)
        {
            var allUserMessages = _messagesRepository.GetAll().OrderByDescending(x => x.DateCreated).Where(x => x.UserFromId == userId || x.UserToId == userId).ToList();

            if (messageFriendId != null && messageFriendId != "")
            {
                allUserMessages = _messagesRepository.GetAll().Where(x => x.UserFromId == messageFriendId || x.UserToId == messageFriendId).ToList();
            }

            return allUserMessages;
        }
        public List<Message> GetAllByFriendId(List<Message> friendMessages, string friendId)
        {
            return friendMessages.Where(x => x.UserFromId == friendId).ToList();
        }
        public bool CheckIfNotSeenMessage(List<Message> friendMessages)
        {
            var exists = friendMessages.Any(x => x.SeenStatus == false);

            return exists;
        }

    }
}
