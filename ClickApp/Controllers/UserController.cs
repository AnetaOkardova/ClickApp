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

        public UserController(UserManager<ApplicationUser> userManager, IUserSkillsService userSkillsService, ISkillsService skillsService)
        {
            _userManager = userManager;
            _userSkillsService = userSkillsService;
            _skillsService = skillsService;
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
                var userInterests = user.Interests.Select(x => x.ToUserInterestViewModel()).ToList();
                userDetails.Interests = userInterests;
            }
            else
            {
                userDetails.Interests = new List<UserInterestViewModel>();
            }

            return View(userDetails);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var userDetails = user.ToUserViewModel();
            if (user.Skills != null)
            {
                //var userSkills = user.Skills.Select(x => x.ToUserSkillViewModel()).ToList();
                //userDetails.Skills = userSkills;
            }
            else
            {
                userDetails.Skills = new List<SkillViewModel>();
            }

            if (user.Interests != null)
            {
                var userInterests = user.Interests.Select(x => x.ToUserInterestViewModel()).ToList();
                userDetails.Interests = userInterests;
            }
            else
            {
                userDetails.Interests = new List<UserInterestViewModel>();
            }

            return View(userDetails);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
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
                }
                else
                {
                    updatedUser.Skills = new List<UserSkill>();
                }

                if (user.Interests != null)
                {
                    var interestsToUpdate = user.Interests.Select(x => x.ToModel()).ToList();
                    updatedUser.Interests = interestsToUpdate;
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
