using ClickApp.Mappings;
using ClickApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ClickApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string userNameSearch, string errorMessage)
        {
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            if (userNameSearch !=null || userNameSearch != "")
            {
                var users = _userManager.Users.Where(x => x.UserName.Contains(userNameSearch)).ToList();
                foreach (var user in users)
                {
                    if(user.Offers == null)
                    {
                        user.Offers = new List<Offer>();
                    }
                }
                var usersForView = users.Select(x => x.ToUserViewModel()).ToList();
                return View(usersForView);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
