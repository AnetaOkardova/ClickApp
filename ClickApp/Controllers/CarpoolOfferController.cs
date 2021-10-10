using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClickApp.Data;
using ClickApp.Models;
using Microsoft.AspNetCore.Identity;
using ClickApp.Services.Interfaces;
using ClickApp.Mappings;

namespace ClickApp.Controllers
{
    public class CarpoolOfferController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICarpoolOfferService _carpoolOfferService;

        public CarpoolOfferController(UserManager<ApplicationUser> userManager, ICarpoolOfferService carpoolOfferService)
        {
            _userManager = userManager;
            _carpoolOfferService = carpoolOfferService;
        }

        public IActionResult Overview()
        {
            var carpoolOffers = _carpoolOfferService.GetAllCarpoolOffers();

            foreach (var carpoolOffer in carpoolOffers)
            {
                if (carpoolOffer.Driver == null)
                {
                    carpoolOffer.Driver = new ApplicationUser();
                }
            }

            var viewCarpoolOffers = carpoolOffers.Select(x => x.ToCarpoolOfferViewModel()).ToList();

            return View(viewCarpoolOffers);
        }


    }
}
