using ClickApp.Mappings;
using ClickApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public IActionResult Index(string userNameSearch, string errorMessage, string subscriptionMessage)
        {
            try
            {
                if (errorMessage != null)
                {
                    ViewBag.ErrorMessage = errorMessage;
                }
                if (subscriptionMessage != null)
                {
                    ViewBag.SubscriptionMessage = subscriptionMessage;
                }
                if (userNameSearch != null || userNameSearch != "")
                {
                    var users = _userManager.Users.Where(x => x.Name.Contains(userNameSearch) || x.LastName.Contains(userNameSearch)).ToList();
                    foreach (var user in users)
                    {
                        if (user.Offers == null)
                        {
                            user.Offers = new List<Offer>();
                        }
                    }
                    var usersForView = users.Select(x => x.ToUserViewModel()).ToList();
                    return View(usersForView);
                }
                return View();
            }
            catch (System.Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
