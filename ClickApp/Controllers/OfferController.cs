using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult GetAllWithFilter(string offerTitle, bool isProffesional, string errorMessage, string successMessage)
        {
            try
            {
                if (errorMessage != null)
                {
                    ViewBag.ErrorMessage = errorMessage;
                }

                if (successMessage != null)
                {
                    ViewBag.SuccessMessage = successMessage;
                }
                var offers = _offersService.GetAllPublicWithFilter(offerTitle, isProffesional);

                var viewOffers = offers.Select(x => x.ToOfferViewModel()).ToList();

                return View(viewOffers);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var offer = new CreateOfferViewModel();
            offer.ValidUntil = DateTime.Now.AddYears(5).Date;
            return View(offer);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOfferViewModel createOfferViewModel,string returnUrl)
        {
            try
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
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("GetAllWithFilter", new { isProffesional = offer.IsProfessional });
                }
                else
                {
                    return View(createOfferViewModel);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            try
            {
                var offer = _offersService.GetById(id);
                if (offer == null)
                {
                    return RedirectToAction("GetAllWithFilter", new { isProffesional = offer.IsProfessional, ErrorMessage = $"There are no active offers with ID {id}." });
                }
                var offerForView = offer.ToOfferViewModel();

                return View(offerForView);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(OfferViewModel offerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var offer = offerViewModel.ToModel();
                    var response = _offersService.Update(offer);

                    if (response.IsSuccessful)
                    {
                        return RedirectToAction("Details", new { id = offer.Id, successMessage = $"The carpool offer with ID {offer.Id} has been successfully updated." });
                    }
                    else
                    {
                        return RedirectToAction("Details", new { id = offer.Id, ErrorMessage = response.Message });
                    }
                }
                else
                {
                    return View(offerViewModel);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var response = _offersService.Delete(id);

                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { successMessage = response.Message });
                }
                return RedirectToAction("Overview", new { errorMessage = response.Message });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> Details(int id, string successMessage, string errorMessage)
        {
            try
            {
                if (successMessage != null)
                {
                    ViewBag.SuccessMessage = successMessage;
                }
                if (errorMessage != null)
                {
                    ViewBag.ErrorMessage = errorMessage;
                }

                var offer = _offersService.GetById(id);
                if (offer == null)
                {
                    return RedirectToAction("Overview", new { ErrorMessage = $"There are no offers with ID {id}." });
                }

                var offerForView = offer.ToOfferViewModel();
                return View(offerForView);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
