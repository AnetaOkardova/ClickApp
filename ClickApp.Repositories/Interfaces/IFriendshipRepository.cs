using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface IFriendshipRepository : IBaseRepository<Friendship>
    {
        Friendship GetByFriendId(string userId, string friendId);
    }
}
