using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickApp.Services
{
    public class FriendshipRequestsService : IFriendshipRequestsService
    {
        private readonly IFriendshipRequestsRepository _friendshipRequestsRepository;
        private readonly IFriendshipService _friendshipService;

        public FriendshipRequestsService(IFriendshipRequestsRepository friendshipRequestsRepository, IFriendshipService friendshipService)
        {
            _friendshipRequestsRepository = friendshipRequestsRepository;
            _friendshipService = friendshipService;
        }
        [Authorize]
        public StatusModel Create(FriendshipRequest friendshipRequest)
        {
            var response = new StatusModel();
            
            var checkIfSentRequest = CheckIfRequestSent(friendshipRequest.UserId, friendshipRequest.RequestedUserId);
            var checkIfReceivedRequest = CheckIfRequestReceived(friendshipRequest.UserId, friendshipRequest.RequestedUserId);

            if (checkIfSentRequest || checkIfReceivedRequest)
            {
                response.IsSuccessful = false;
                response.Message = $"A friend request has already been sent.";
                return response;
            }
            friendshipRequest.ActiveRequest = true;
            _friendshipRequestsRepository.Create(friendshipRequest);
            return response;
        }

        public List<FriendshipRequest> GetAll()
        {
            return _friendshipRequestsRepository.GetAll();
        }

        public List<FriendshipRequest> GetByUserId(string userId)
        {
            return _friendshipRequestsRepository.GetByUserId(userId);
        }

        public List<FriendshipRequest> GetAllActiveWithFilter(string userId, string RequestedUserId)
        {
            return _friendshipRequestsRepository.GetAllActiveWithFilter(userId, RequestedUserId);
        }
        public bool CheckIfRequestSent(string userId, string requestedUserId)
        {
            var friendshipRequest = _friendshipRequestsRepository.RequestSent(userId, requestedUserId);
            if (friendshipRequest == null)
            {
                return false;
            }
            return true;
        }
        public bool CheckIfRequestReceived(string userId, string requestedUserId)
        {
            var friendshipRequest =  _friendshipRequestsRepository.RequestReceived(userId, requestedUserId);
            if(friendshipRequest == null)
            {
                return false;
            }
            return true;
        }
        public StatusModel Update(FriendshipRequest friendshipRequest)
        {
            var response = new StatusModel();
            var friendshipRequestFromDb = _friendshipRequestsRepository.GetById(friendshipRequest.Id);
            if (friendshipRequestFromDb == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The friendship request with ID {friendshipRequest.Id} is not found.";
            }
            else if(friendshipRequest.ActiveRequest == false)
            {
                friendshipRequestFromDb.ActiveRequest = false;
                friendshipRequestFromDb.RequestConfirmed = false;
                friendshipRequestFromDb.DateRequestConfirmed = DateTime.Now;
                _friendshipRequestsRepository.Update(friendshipRequestFromDb);
            }
            else
            {
                friendshipRequestFromDb.RequestConfirmed = true;
                friendshipRequestFromDb.DateRequestConfirmed = DateTime.Now;
                friendshipRequestFromDb.ActiveRequest = false;

                _friendshipRequestsRepository.Update(friendshipRequestFromDb);
                response.Message = $"The friednship request with ID {friendshipRequest.Id} has been successfully updated.";
            }
            return response;
        }

        public StatusModel DeclineRequest(string userId, string requestedUserId)
        {
            var response = new StatusModel();
            var friendshipRequest = _friendshipRequestsRepository.RequestReceived(userId, requestedUserId);
            if (friendshipRequest != null)
            {
                friendshipRequest.ActiveRequest = false;
                _friendshipRequestsRepository.Update(friendshipRequest);
                response.Message = "The friednship request has been declined.";
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = $"The friendship request is not found.";
            }
            return response;
        }

        public async Task<StatusModel> AcceptRequestAsync(string userId, string requestedUserId, ApplicationUser user)
        {
            var response = new StatusModel();
            var friendshipRequest = _friendshipRequestsRepository.RequestReceived(userId, requestedUserId);
            if (friendshipRequest != null)
            {
                friendshipRequest.ActiveRequest = false;
                friendshipRequest.DateRequestConfirmed = DateTime.Now;
                friendshipRequest.RequestConfirmed = true;
                _friendshipRequestsRepository.Update(friendshipRequest);
                var friendship = new Friendship();
                friendship.UserId = userId;
                friendship.User = user;
                friendship.FriendsId = requestedUserId;
                var responseForCreatingFriendship = await _friendshipService.CreateAsync(friendship);
                if (responseForCreatingFriendship.IsSuccessful)
                {
                response.Message = "The friednship request has been accepted. Congrats on your new friend.";
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message = responseForCreatingFriendship.Message;
                }

            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "The friendship request is not found.";
            }
            return response;
        }

        public List<FriendshipRequest> GetAllUserFriendRequests(ApplicationUser user)
        {
            var friendRequests = _friendshipRequestsRepository.GetAllUserFriendRequests(user);
            return friendRequests;
        }

        public StatusModel CancelRequest(string id, string requestedUserId)
        {
            var response = new StatusModel();
            var friendshipRequestSent = _friendshipRequestsRepository.RequestSent(id, requestedUserId);
            if (friendshipRequestSent != null)
            {
                friendshipRequestSent.ActiveRequest = false;
                _friendshipRequestsRepository.Update(friendshipRequestSent);
                response.Message = "The friednship request with has been canceled.";
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "The friendship request is not found.";
            }
            return response;
        }
    }
}
