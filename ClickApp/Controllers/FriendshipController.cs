using ClickApp.Models;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class FriendshipController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFriendshipRequestsService _friendshipRequestsService;
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(UserManager<ApplicationUser> userManager, IFriendshipRequestsService friendshipRequestsService, IFriendshipService friendshipService)
        {
            _userManager = userManager;
            _friendshipRequestsService = friendshipRequestsService;
            _friendshipService = friendshipService;
        }
        public IActionResult Unfriend(string userId, string requestedUserId)
        {
            var response = _friendshipService.Delete(userId, requestedUserId);
            if (!response.IsSuccessful)
            {
                return RedirectToAction("Details", "User", new { userId = requestedUserId, ErrorMessage = response.Message });
            }

            return RedirectToAction("Details", "User", new { userId = requestedUserId, SuccessMessage = response.Message });
        }
    }
}
