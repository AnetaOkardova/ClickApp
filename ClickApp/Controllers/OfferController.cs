using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOffersService _offersService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OfferController(IOffersService offersService, UserManager<ApplicationUser> userManager)
        {
            _offersService = offersService;
            _userManager = userManager;
        }
        public IActionResult Overview()
        {
            return View();
        }
        public IActionResult GetAllWithFilter(string offerTitle, bool isProffesional)
        {
            var offers = _offersService.GetAllPublicWithFilter(offerTitle, isProffesional);

            var viewOffers = offers.Select(x => x.ToOfferViewModel()).ToList();

            return View(viewOffers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var offer = new CreateOfferViewModel();
            offer.ValidUntil = DateTime.Now.AddYears(5).Date;
            return View(offer);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOfferViewModel createOfferViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = User;
                var userFromDb = await _userManager.FindByNameAsync(user.Identity.Name);

                var offer = createOfferViewModel.ToModel();
                offer.DateCreated = DateTime.Now;
                offer.UserId = userFromDb.Id;
                offer.User = userFromDb;
                _offersService.Create(offer);

                return RedirectToAction("GetAllWithFilter", new { isProffesional = offer.IsProfessional});
                //return URL
            }
            else
            {
                return View(createOfferViewModel);
            }
        }
    }
}
