using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public SubscriptionController(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }
        public IActionResult CreateSubscription(string subscriberEmail, string returnUrl)
        {
            if(subscriberEmail !=null && subscriberEmail != "")
            {
                var response = _subscriptionsService.Create(subscriberEmail);
                if (!response.IsSuccessful)
                {
                    return RedirectToAction("Index", "Home", new { SubscriptionMessage = response.Message});
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
