using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface IFriendshipRequestsRepository : IBaseRepository<FriendshipRequest>
    {
        List<FriendshipRequest> GetAllWithFilter(string userId, string requestedUserId);
        FriendshipRequest RequestSent(string userId, string requestedUserId);
        FriendshipRequest RequestReceived(string userId, string requestedUserId);
    }
}
