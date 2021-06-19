using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class FriendshipRequestsRepository : BaseRepository<FriendshipRequest>, IFriendshipRequestsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public FriendshipRequestsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<FriendshipRequest> GetAllActiveWithFilter(string userId, string requestedUserId)
        {
            var friendshipRequests = GetAll();
            friendshipRequests.Where(x => (x.UserId == userId && x.RequestedUserId == requestedUserId && x.ActiveRequest == true) || (x.UserId == requestedUserId && x.RequestedUserId == userId && x.ActiveRequest == true))
                .ToList();

            return friendshipRequests;

        }

        public FriendshipRequest RequestSent(string userId, string requestedUserId)
        {
            var friendshipRequest = GetAll();
            return friendshipRequest.FirstOrDefault(x => x.UserId == userId && x.RequestedUserId == requestedUserId && x.ActiveRequest == true); 
        }

        public FriendshipRequest RequestReceived(string userId, string requestedUserId)
        {
            var friendshipRequest = GetAll();

            return friendshipRequest.FirstOrDefault(x => x.UserId == requestedUserId && x.RequestedUserId == userId && x.ActiveRequest == true);
        }

        public List<FriendshipRequest> GetByUserId(string userId)
        {
            var friendshipRequests = GetAll();
            friendshipRequests.Where(x => (x.UserId == userId) || (x.RequestedUserId == userId))
                .ToList();

            return friendshipRequests; ;
        }
    }
}
