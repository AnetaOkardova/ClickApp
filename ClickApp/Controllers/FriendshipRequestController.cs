using ClickApp.Models;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [Authorize]
        public async Task<IActionResult> Create(string requestedUserId)
        {
            try
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
                    return RedirectToAction("Details", "User", new { userId = requestedUserId, SuccessMessage = "The friend request has been sent." });
                }
                else
                {
                    return RedirectToAction("Details", "User", new { userId = requestedUserId, ErrorMessage = response.Message });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            
        }
        [Authorize]
        public async Task<IActionResult> Delete(string requestedUserId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var response = _friendshipRequestsService.CancelRequest(user.Id, requestedUserId);

                if (!response.IsSuccessful)
                {
                    return RedirectToAction("Details", "User", new { userId = requestedUserId, ErrorMessage = response.Message });
                }

                return RedirectToAction("Details", "User", new { userId = requestedUserId, SuccessMessage = response.Message });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize]
        public async Task<IActionResult> Update(string requestedUserId, bool requestDecision)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (requestDecision != null)
                {
                    if (requestDecision)
                    {
                        var response = await _friendshipRequestsService.AcceptRequestAsync(user.Id, requestedUserId, user);
                        if (!response.IsSuccessful)
                        {
                            return RedirectToAction("Details", "User", new { userId = requestedUserId, ErrorMessage = response.Message });
                        }

                        return RedirectToAction("Details", "User", new { userId = requestedUserId, SuccessMessage = response.Message });
                    }
                    else
                    {
                        var response = _friendshipRequestsService.DeclineRequest(user.Id, requestedUserId);
                        if (!response.IsSuccessful)
                        {
                            return RedirectToAction("Details", "User", new { userId = requestedUserId, ErrorMessage = response.Message });
                        }

                        return RedirectToAction("Details", "User", new { userId = requestedUserId, SuccessMessage = response.Message });
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
