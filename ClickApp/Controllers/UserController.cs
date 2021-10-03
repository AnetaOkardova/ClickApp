using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserSkillsService _userSkillsService;
        private readonly ISkillsService _skillsService;
        private readonly IInterestsService _interestsService;
        private readonly IUserInterestsService _userInterestsService;
        private readonly IFriendshipService _friendshipService;
        private readonly IFriendshipRequestsService _friendshipRequestsService;
        private readonly IOffersService _offersService;

        public UserController(UserManager<ApplicationUser> userManager, IUserSkillsService userSkillsService, ISkillsService skillsService, IInterestsService interestsService, IUserInterestsService userInterestsService, IFriendshipService friendshipService, IFriendshipRequestsService friendshipRequestsService, IOffersService offersService)
        {
            _userManager = userManager;
            _userSkillsService = userSkillsService;
            _skillsService = skillsService;
            _interestsService = interestsService;
            _userInterestsService = userInterestsService;
            _friendshipService = friendshipService;
            _friendshipRequestsService = friendshipRequestsService;
            _offersService = offersService;
        }
        [Authorize]
        public async Task<IActionResult> Details(string userId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (userId!=null && userId != "")
            {
                user = await _userManager.FindByIdAsync(userId);
            }
            
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }
            user.Skills = _userSkillsService.GetAllSkillsForUser(user.Id);
            user.Interests = _userInterestsService.GetAllSkillsForUser(user.Id);
            user.Offers = _offersService.GetAllOffersForUser(user.Id);
            
            var userDetails = user.ToUserViewModel();

            if (user.Skills != null)
            {
                var userSkillIds = user.Skills.Select(x => x.SkillId).ToList();
                var skills = new List<Skill>();
                foreach (var id in userSkillIds)
                {
                    var skill = _skillsService.GetById(id);
                    skills.Add(skill);
                }

                var userSkillsForView = skills.Select(x => x.ToSkillViewModel()).ToList();
                userDetails.Skills = userSkillsForView;
                
            }
            else
            {
                userDetails.Skills = new List<SkillViewModel>();
            }

            if (user.Interests != null)
            {
                var userInterestIds = user.Interests.Select(x => x.InterestId).ToList();
                var interests = new List<Interest>();
                foreach (var id in userInterestIds)
                {
                    var interest = _interestsService.GetById(id);
                    interests.Add(interest);
                }

                var userInterestsForView = interests.Select(x => x.ToInterestViewModel()).ToList();
                userDetails.Interests = userInterestsForView;
            }
            else
            {
                userDetails.Interests = new List<InterestViewModel>();
            }
            var friendsListView = new List<UserCardViewModel>();

            var friends = _friendshipService.GetAllUserFriendships(user);
            if (friends != null)
            {
                foreach (var friendship in friends)
                {
                    var friend = await _userManager.FindByIdAsync(friendship.FriendsId);
                    var friendCardModel = friend.ToUserCardViewModel();
                    friendsListView.Add(friendCardModel);
                }
            }
            userDetails.Friends = friendsListView;

            var friendRequestsListView = new List<UserCardViewModel>();

            var friendRequests = _friendshipRequestsService.GetAllUserFriendRequests(user);
            if (friendRequests != null)
            {
                foreach (var friendRequest in friendRequests)
                {
                    var friend = await _userManager.FindByIdAsync(friendRequest.UserId); ;
                    var friendCardModel = friend.ToUserCardViewModel();
                    friendRequestsListView.Add(friendCardModel);
                }
            }
            userDetails.RequestingUsers = friendRequestsListView;
            return View(userDetails);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Skills = _userSkillsService.GetAllSkillsForUser(user.Id);
            user.Interests = _userInterestsService.GetAllSkillsForUser(user.Id);
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var userDetails = user.ToEditUserViewModel();
            if (user.Skills != null)
            {
                var listOfUserSkills = new List<SkillViewModel>();
                foreach (var userSkill in user.Skills)
                {
                    var skill = _skillsService.GetById(userSkill.SkillId).ToSkillViewModel();
                    listOfUserSkills.Add(skill);
                }

                userDetails.Skills = listOfUserSkills;
            }
            else
            {
                userDetails.Skills = new List<SkillViewModel>();
            }

            if (user.Interests != null)
            {
                var listOfUserInterests = new List<InterestViewModel>();
                foreach (var userIngterest in user.Interests)
                {
                    var interest = _interestsService.GetById(userIngterest.InterestId).ToInterestViewModel();
                    listOfUserInterests.Add(interest);
                }

                userDetails.Interests = listOfUserInterests;
            }
            else
            {
                userDetails.Interests = new List<InterestViewModel>();
            }
            
            //var interests = _interestsService.GetAll();
            //var interestsForView = interests.Select(x => x.ToInterestViewModel()).ToList();
            //userDetails.AllInterests = interestsForView;

            return View(userDetails);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(user.Id);
                if (userFromDb == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                var updatedUser = user.ToModel();


                if (user.Skills != null)
                {
                    //var skillsToUpdate = user.Skills.Select(x => x.ToModel()).ToList();
                    //updatedUser.Skills = skillsToUpdate;

                    var listOfUserSkills = new List<UserSkill>();
                    foreach (var userSkill in user.Skills)
                    {
                        var skill = _userSkillsService.GetById(userSkill.Id);
                        listOfUserSkills.Add(skill);
                    }

                    updatedUser.Skills = listOfUserSkills;
                }
                else
                {
                    updatedUser.Skills = new List<UserSkill>();
                }

                if (user.Interests != null)
                {
                    var listOfUserInterests = new List<UserInterest>();
                    foreach (var userInterest in user.Interests)
                    {
                        var interest = _userInterestsService.GetById(userInterest.Id);
                        listOfUserInterests.Add(interest);
                    }

                    updatedUser.Interests = listOfUserInterests;
                }
                else
                {
                    updatedUser.Interests = new List<UserInterest>();
                }

                userFromDb.Name = updatedUser.Name;
                userFromDb.LastName = updatedUser.LastName;
                userFromDb.Address = updatedUser.Address;
                userFromDb.City = updatedUser.City;
                userFromDb.Country = updatedUser.Country;
                userFromDb.ProfilePhotoURL = updatedUser.ProfilePhotoURL;
                userFromDb.Description = updatedUser.Description;
                userFromDb.Skills = updatedUser.Skills;
                userFromDb.Interests = updatedUser.Interests;


                await _userManager.UpdateAsync(userFromDb);

                return RedirectToAction("Details");
            }
            else
            {
                return View(user);
            }
        }
    }
}
