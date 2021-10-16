using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessagesService _messagesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(IMessagesService messagesService, UserManager<ApplicationUser> userManager)
        {
            _messagesService = messagesService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Overview(string userId, string messageFriendId, List<string> listOfIds, bool openMessages)
        {

            if(listOfIds.Count != 0)
            {
                userId = listOfIds[0];
                messageFriendId = listOfIds[1];
            }
            var messagesFriendsWithStatusList = new List<MessageFriendsViewModel>();
            var allMessagesBySelectedFriend = new List<Message>();
            var newMessage = new CreateMessageViewModel();

            var allMessagesForUser = _messagesService.GetAllMessagesWithFriend(userId, null);

            if (userId != null || userId != "")
            {
                var messageFriendsList = new List<ApplicationUser>();

                foreach (var message in allMessagesForUser.OrderByDescending(x => x.DateCreated))
                {
                    if (message.UserFromId == userId)
                    {
                        var friend = await _userManager.FindByIdAsync(message.UserToId);
                        var messageSeenStatus = true;

                        var exists = messageFriendsList.Any(x => x.Id == message.UserToId);
                        if (!exists)
                        {
                            messageFriendsList.Add(friend);

                            var messageFriendForView = new MessageFriendsViewModel();
                            messageFriendForView.MessagesFriend = friend;
                            messageFriendForView.LastSeenStatus = messageSeenStatus;
                            messagesFriendsWithStatusList.Add(messageFriendForView);
                        }
                    }
                    else if (message.UserToId == userId)
                    {
                        var friend = await _userManager.FindByIdAsync(message.UserFromId);
                        var messageSeenStatus = message.SeenStatus;

                        var exists = messageFriendsList.Any(x => x.Id == message.UserFromId);
                        if (!exists)
                        {
                            messageFriendsList.Add(friend);

                            var messageFriendForView = new MessageFriendsViewModel();
                            messageFriendForView.MessagesFriend = friend;
                            messageFriendForView.LastSeenStatus = messageSeenStatus;
                            messagesFriendsWithStatusList.Add(messageFriendForView);
                        }
                    }
                }
            }

            if (messageFriendId != null && messageFriendId != "")
            {
                newMessage.UserFromId = userId;
                newMessage.UserToId = messageFriendId;
                newMessage.Content = null;

                var allMessagesBySelectedFriendSearch = allMessagesForUser.Where(x => x.UserFromId == messageFriendId || x.UserToId == messageFriendId).ToList();
                if (allMessagesBySelectedFriendSearch.Count != 0 && allMessagesBySelectedFriendSearch != null)
                {
                    allMessagesBySelectedFriend = allMessagesBySelectedFriendSearch;
                }
                else
                {
                    var message = new Message();
                    message.UserFromId = userId;
                    message.UserToId = messageFriendId;
                    message.Content = null;
                    message.SeenStatus = false;
                    message.DateCreated = DateTime.Now;
                    allMessagesBySelectedFriend.Add(message);
                }
            }

            var messagesForView = new MessagesViewModel();
            messagesForView.MessageFriends = messagesFriendsWithStatusList;
            messagesForView.Messages = allMessagesBySelectedFriend;
            messagesForView.NewMessage = newMessage;

            if (openMessages)
            {
                var allCheckedMessages = _messagesService.GetAllMessagesWithFriend(userId, messageFriendId);
                foreach (var message in allCheckedMessages)
                {
                    message.SeenStatus = true;
                    _messagesService.UpdateMessage(message);
                }
            }
            return View(messagesForView);
        }
        
        [HttpPost]
        public IActionResult CreateMessage(string userId, string messageFriendId, string content)
        {
            if (userId != null && messageFriendId != null && content != null)
            {
                _messagesService.CreateMessage(userId, messageFriendId, content);
                return RedirectToAction("Overview", "Message", new { userId, messageFriendId });
            }
            else
            {
                return RedirectToAction("Overview", new { userId = userId });
            }
        }
    }
}
