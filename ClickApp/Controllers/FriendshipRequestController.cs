using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    [Authorize]
    public class FriendshipRequestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFriendshipRequestsService _friendshipRequestsService;

        public FriendshipRequestController(UserManager<ApplicationUser> userManager, IFriendshipRequestsService friendshipRequestsService)
        {
            _userManager = userManager;
            _friendshipRequestsService = friendshipRequestsService;
        }
        public async Task<IActionResult> Create(string requestedUserId)
        {
            var user = await _userManager.GetUserAsync(User);

            var newFriendshipRequest = new FriendshipRequest();
            newFriendshipRequest.RequestCreated = DateTime.Now;
            newFriendshipRequest.DateRequestConfirmed = DateTime.Now;
            newFriendshipRequest.User = user;
            newFriendshipRequest.UserId = user.Id;
            newFriendshipRequest.RequestedUserId = requestedUserId;

            var response = _friendshipRequestsService.Create(newFriendshipRequest);
            if (response.IsSuccessful)
            {
                return RedirectToAction("Index", "Home", new { SuccessMessage = $"The friend request has been sent." });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { ErrorMessage = response.Message });
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Delete(string requestedUserId)
        {
            var user = await _userManager.GetUserAsync(User);

            var response = _friendshipRequestsService.DeclineRequest(user.Id, requestedUserId);

            if (!response.IsSuccessful)
            {
                return RedirectToAction("Index", "Home", new { ErrorMessage = response.Message });
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Update(string requestedUserId, bool requestDecision)
        {
            var user = await _userManager.GetUserAsync(User);
            if (requestDecision != null)
            {
                if (requestDecision)
                {
                    var response = _friendshipRequestsService.AcceptRequest(user.Id, requestedUserId);
                    if (!response.IsSuccessful)
                    {
                        return RedirectToAction("Index", "Home", new { ErrorMessage = response.Message });
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var response = _friendshipRequestsService.DeclineRequest(user.Id, requestedUserId);
                    if (!response.IsSuccessful)
                    {
                        return RedirectToAction("Index", "Home", new { ErrorMessage = response.Message });
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
