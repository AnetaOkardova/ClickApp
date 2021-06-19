using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClickApp.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<StatusModel> CreateAsync(Friendship friendship);

        StatusModel Delete(string userId, string requestedUserId);

        List<Friendship> GetAll();

        Friendship GetByFriendId(string userId, string friendId);
        List<Friendship> GetAllUserFriendships(ApplicationUser user);
    }
}
