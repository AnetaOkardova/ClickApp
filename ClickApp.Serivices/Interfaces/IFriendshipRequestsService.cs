using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClickApp.Services.Interfaces
{
    public interface IFriendshipRequestsService
    {
        StatusModel Create(FriendshipRequest friendshipRequest);
        List<FriendshipRequest> GetAll();
        List<FriendshipRequest> GetByUserId(string userId);
        List<FriendshipRequest> GetAllActiveWithFilter(string userId, string requestedUserId);
        StatusModel Update(FriendshipRequest requestedUserId);
        bool CheckIfRequestSent(string userId, string requestedUserId);
        bool CheckIfRequestReceived(string userId, string requestedUserId);
        StatusModel DeclineRequest(string userId, string requestedUserId);
        Task<StatusModel> AcceptRequestAsync(string userId, string requestedUserId, ApplicationUser user);
    }
}
