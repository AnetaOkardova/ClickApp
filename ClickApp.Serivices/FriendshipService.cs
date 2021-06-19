using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickApp.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public FriendshipService(IFriendshipRepository friendshipRepository, UserManager<ApplicationUser> userManager)
        {
            _friendshipRepository = friendshipRepository;
            _userManager = userManager;
        }

        public async Task<StatusModel> CreateAsync(Friendship friendship)
        {
            var response = new StatusModel();

            var friendshipFromDb = _friendshipRepository.GetByFriendId(friendship.UserId, friendship.FriendsId);

            if (friendshipFromDb != null && friendshipFromDb.FriendShipStatus == true)
            {
                response.IsSuccessful = false;
                response.Message = "You already are friends.";
                return response;
            }
            if (friendshipFromDb != null && friendshipFromDb.FriendShipStatus == false)
            {
                var friendsFriendshipFromDb = _friendshipRepository.GetByFriendId(friendship.FriendsId, friendship.UserId);

                friendshipFromDb.FriendShipStatus = true;
                friendshipFromDb.FrienshipCreated = DateTime.Now;

                friendsFriendshipFromDb.FriendShipStatus = true;
                friendsFriendshipFromDb.FrienshipCreated = DateTime.Now;

                _friendshipRepository.Update(friendshipFromDb);
                _friendshipRepository.Update(friendsFriendshipFromDb);

                return response;
            }
            friendship.FriendShipStatus = true;
            friendship.FrienshipCreated = DateTime.Now;
            _friendshipRepository.Create(friendship);

            var friendsFriendship = new Friendship();
            var friendUser = await _userManager.FindByIdAsync(friendship.FriendsId);
            friendsFriendship.User = friendUser;
            friendsFriendship.UserId = friendship.FriendsId;
            friendsFriendship.FriendsId = friendship.UserId;
            friendsFriendship.FriendShipStatus = true;
            friendsFriendship.FrienshipCreated = DateTime.Now;
            _friendshipRepository.Create(friendsFriendship);

            return response;
        }

        public StatusModel Delete(string userId, string requestedUserId)
        {
            var response = new StatusModel();
            var friendshipFromDb = _friendshipRepository.GetByFriendId(userId, requestedUserId);
            if (friendshipFromDb == null || friendshipFromDb.FriendShipStatus == false)
            {
                response.IsSuccessful = false;
                response.Message = "You are not friends yet";
            }
            else
            {
                friendshipFromDb.FriendShipStatus = false;
                friendshipFromDb.FrienshipEnded = DateTime.Now;
                _friendshipRepository.Update(friendshipFromDb);

                var friendsFriendshipFromDb = _friendshipRepository.GetByFriendId(requestedUserId, userId);
                friendsFriendshipFromDb.FriendShipStatus = false;
                friendsFriendshipFromDb.FrienshipEnded = DateTime.Now;
                _friendshipRepository.Update(friendsFriendshipFromDb);

                response.Message = "You are no longer friends.";
            }
            return response;
        }

        public List<Friendship> GetAll()
        {
            return _friendshipRepository.GetAll();
        }

        public List<Friendship> GetAllUserFriendships(ApplicationUser user)
        {
            return _friendshipRepository.GetAllUserFriendships(user);
        }

        public Friendship GetByFriendId(string userId, string friendId)
        {
            return _friendshipRepository.GetByFriendId(userId, friendId);
        }


        //public StatusModel Update(Friendship friendship)
        //{
        //    var response = new StatusModel();
        //    var friendshipFromDB = _friendshipRepository.GetByFriendId(friendship.UserId, friendship.FriendsId);

        //    if (friendshipFromDB == null)
        //    {
        //        response.IsSuccessful = false;
        //        response.Message = "You are not friends.";
        //    }
        //    else
        //    {
        //        //What are the changes
        //        response.Message = $"The friendship has been successfully updated.";
        //    }
        //    return response;
        //}
    }
}
