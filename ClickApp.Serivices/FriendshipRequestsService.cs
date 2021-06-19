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
            //var friendshipRequests = _friendshipRequestsRepository.GetAll();

            //var checkIfSentRequest = friendshipRequests.Any(x => x.UserId == friendshipRequest.UserId && x.RequestedUserId == friendshipRequest.RequestedUserId && x.ActiveRequest == true);
            //var checkIfReceivedRequest = friendshipRequests.Any(x => x.UserId == friendshipRequest.RequestedUserId && x.RequestedUserId == friendshipRequest.UserId && x.ActiveRequest == true);


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

        public FriendshipRequest GetById(int id)
        {
            return _friendshipRequestsRepository.GetById(id);
        }

        public List<FriendshipRequest> GetAllWithFilter(string userId, string RequestedUserId)
        {
            return _friendshipRequestsRepository.GetAllWithFilter(userId, RequestedUserId);
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
                response.Message = $"The friednship request with ID {friendshipRequest.Id} has been declined.";
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = $"The friendship request is not found.";
            }
            return response;
        }

        public StatusModel AcceptRequest(string userId, string requestedUserId)
        {
            var response = new StatusModel();
            var friendshipRequest = _friendshipRequestsRepository.RequestReceived(userId, requestedUserId);
            if (friendshipRequest != null)
            {
                friendshipRequest.ActiveRequest = false;
                friendshipRequest.DateRequestConfirmed = DateTime.Now;
                friendshipRequest.RequestConfirmed = true;
                _friendshipRequestsRepository.Update(friendshipRequest);
                response.Message = $"The friednship request with ID {friendshipRequest.Id} has been accepted.";
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = $"The friendship request is not found.";
            }
            return response;
        }
    }
}
