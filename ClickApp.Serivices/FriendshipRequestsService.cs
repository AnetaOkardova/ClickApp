using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Services
{
    public class FriendshipRequestsService : IFriendshipRequestsService
    {
        private readonly IFriendshipRequestsRepository _friendshipRequestsRepository;

        public FriendshipRequestsService(IFriendshipRequestsRepository friendshipRequestsRepository)
        {
            _friendshipRequestsRepository = friendshipRequestsRepository;
        }
        [Authorize]
        public StatusModel Create(FriendshipRequest friendshipRequest)
        {
            var response = new StatusModel();
            var friendshipRequests = _friendshipRequestsRepository.GetAll();

            var checkIfSentRequest = friendshipRequests.Any(x => x.UserId == friendshipRequest.UserId && x.RequestedUserId == friendshipRequest.RequestedUserId && x.ActiveRequest == true);
            var checkIfReceivedRequest = friendshipRequests.Any(x => x.UserId == friendshipRequest.RequestedUserId && x.RequestedUserId == friendshipRequest.UserId && x.ActiveRequest == true);

            if (checkIfSentRequest || checkIfReceivedRequest)
            {
                response.IsSuccessful = false;
                response.Message = $"A friend request has already been sent.";
                return response;
            }

            _friendshipRequestsRepository.Create(friendshipRequest);
            return response;
        }

        public StatusModel Delete(int id)
        {
            var response = new StatusModel();
            var friendshipRequest = _friendshipRequestsRepository.GetById(id);
            if (friendshipRequest == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The friendship request with ID {id} is not found.";
            }
            else
            {
                _friendshipRequestsRepository.Delete(friendshipRequest);
                response.Message = $"The friendshipRequest has been canceled";
            }
            return response;
        }

        public List<FriendshipRequest> GetAll()
        {
            return _friendshipRequestsRepository.GetAll();
        }

        public FriendshipRequest GetById(int id)
        {
            return _friendshipRequestsRepository.GetById(id);
        }

        public List<FriendshipRequest> GetAllWithFilter(string userId, string RequestedUserId)
        {
            return _friendshipRequestsRepository.GetAllWithFilter(userId, RequestedUserId);
        }
        public bool CheckIfRequestSent(string userId, string RequestedUserId)
        {
            var friendshipRequest = _friendshipRequestsRepository.CheckIfRequestSent(userId, RequestedUserId);
            if (friendshipRequest == null)
            {
                return false;
            }
            return true;
        }
        public bool CheckIfRequestReceived(string userId, string RequestedUserId)
        {
            var friendshipRequest =  _friendshipRequestsRepository.CheckIfRequestReceived(userId, RequestedUserId);
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
    }
}
