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
        StatusModel Delete(int id);
        List<FriendshipRequest> GetAll();
        FriendshipRequest GetById(int id);
        List<FriendshipRequest> GetAllWithFilter(string userId, string RequestedUserId);
        StatusModel Update(FriendshipRequest friendshipRequest);
        bool CheckIfRequestSent(string userId, string RequestedUserId);
        bool CheckIfRequestReceived(string userId, string RequestedUserId);
    }
}
