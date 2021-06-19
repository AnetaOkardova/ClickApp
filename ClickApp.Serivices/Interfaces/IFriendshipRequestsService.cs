using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IFriendshipRequestsService
    {
        StatusModel Create(FriendshipRequest friendshipRequest);
        List<FriendshipRequest> GetAll();
        FriendshipRequest GetById(int id);
        List<FriendshipRequest> GetAllWithFilter(string userId, string requestedUserId);
        StatusModel Update(FriendshipRequest requestedUserId);
        bool CheckIfRequestSent(string userId, string requestedUserId);
        bool CheckIfRequestReceived(string userId, string requestedUserId);
        StatusModel DeclineRequest(string userId, string requestedUserId);
        StatusModel AcceptRequest(string userId, string requestedUserId);
    }
}
