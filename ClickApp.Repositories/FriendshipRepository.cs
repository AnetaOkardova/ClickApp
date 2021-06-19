using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository
    {
        private ApplicationDbContext _context { get; set; }
        public FriendshipRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Friendship GetByFriendId(string userId, string friendId)
        {
            var allFrienships = GetAll();
            if(allFrienships.Count() == 0)
            {
                var friendship =  new Friendship();
                friendship = null;
                return friendship;
            }
            else
            {
                var friendshipInDB = allFrienships.FirstOrDefault(x => x.UserId == userId && x.FriendsId == friendId);
                return friendshipInDB;
            }
        }
    }
}
