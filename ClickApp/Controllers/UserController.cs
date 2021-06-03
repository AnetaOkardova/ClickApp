using ClickApp.Mappings;
using ClickApp.Models;
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

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Overview()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var userDetails = user.ToUserViewModel();
            if(user.Skills != null)
            {
                var userSkills = user.Skills.Select(x => x.ToUserSkillViewModel()).ToList();
                userDetails.Skills = userSkills;
            }
            else
            {
                userDetails.Skills = new List<UserSkillViewModel>();
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
    }
}
