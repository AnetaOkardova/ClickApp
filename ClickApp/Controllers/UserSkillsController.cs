using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class UserSkillsController : Controller
    {
        private readonly IUserSkillsService _userSkillsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISkillsService _skillsService;
        public UserSkillsController(IUserSkillsService userSkillsService, UserManager<ApplicationUser> userManager, ISkillsService skillsService)
        {
            _userSkillsService = userSkillsService;
            _userManager = userManager;
            _skillsService = skillsService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditUserSkills(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var userDetails = user.ToEditUserViewModel();

            user.Skills = _userSkillsService.GetAllSkillsForUser(user.Id);
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

            var skills = _skillsService.GetAll();
            var skillsForView = skills.Select(x => x.ToSkillViewModel()).ToList();
            userDetails.AllSkills = skillsForView;

            return View(userDetails);
            //return RedirectToAction("Details", "User", new { userId = userId });
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
                    //var skillsToUpdate = user.Skills.Select(x => x.ToUserSkillModel()).ToList();
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
                        //var interest = _userInterestsService.GetById(userInterest.Id);
                        //listOfUserInterests.Add(interest);
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
