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
using Microsoft.AspNetCore.Authorization;
using ClickApp.ViewModels;

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

        public IActionResult Overview(string successMessage, string errorMessage)
        {
            if (successMessage != null)
            {
                ViewBag.SuccessMessage = successMessage;
            }
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            var carpoolOffers = _carpoolOfferService.GetAllCarpoolOffers();

            var activeCarpoolOffers = carpoolOffers.Where(x => x.SeatsAvailable > 0).ToList();

            var viewCarpoolOffers = activeCarpoolOffers.Select(x => x.ToCarpoolOfferViewModel()).ToList();

            return View(viewCarpoolOffers);
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var carpoolOffer = _carpoolOfferService.GetById(id);
            if (carpoolOffer == null)
            {
                return RedirectToAction("Overview", new { ErrorMessage = $"There are no active carpool offers with ID {id}." });

            }
            var carpoolOfferForView = carpoolOffer.ToCarpoolOfferViewModel();

            return View(carpoolOfferForView);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CarpoolOfferViewModel carpoolOfferViewModel)
        {
            if (ModelState.IsValid)
            {
                var carpoolOffer = carpoolOfferViewModel.ToModel();
                var response = _carpoolOfferService.Update(carpoolOffer);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { SuccessMessage = $"The carpool offer has been successfully created." });
                }
                else
                {
                    return RedirectToAction("Overview", new { ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(carpoolOfferViewModel);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var carpoolOffer = _carpoolOfferService.GetById(id);

            
            if (carpoolOffer == null)
            {
                return RedirectToAction("Overview", new { ErrorMessage = $"There are no active carpool offers with ID {id}." });

            }
            if (carpoolOffer.DriverId != user.Id)
            {
                return RedirectToAction("Overview", new { ErrorMessage = $"You are not authorized to see the carpool offers with ID {id}." });
            }
            var carpoolOfferForView = carpoolOffer.ToCarpoolOfferViewModel();

            var requestingPassengers = new List<ApplicationUser>();
            var requestingPassengersForView = new List<CarpoolUserViewModel>();


            //if (carpoolOffer.RequestingPassengers.Count != 0 && carpoolOffer.RequestingPassengers != null)
            //{
            //    foreach (var passengerRequest in carpoolOffer.RequestingPassengers)
            //    {
            //        var passenger = await _userManager.FindByIdAsync(passengerRequest.RequestingPassengerId);
            //        requestingPassengers.Add(passenger);
            //    }
            //    requestingPassengersForView = requestingPassengers.Select(x => x.ToCarpoolUserViewModel()).ToList();
            //}

            var acceptedPassengers = new List<ApplicationUser>();
            var acceptedPassengersForView = new List<CarpoolUserViewModel>();

            //if (carpoolOffer.AcceptedPassengers.Count != 0 && carpoolOffer.AcceptedPassengers != null)
            //{
            //    foreach (var acceptedPassenger in carpoolOffer.AcceptedPassengers)
            //    {
            //        var passenger = await _userManager.FindByIdAsync(acceptedPassenger.AcceptedPassengerId);
            //        acceptedPassengers.Add(passenger);
            //    }
            //    acceptedPassengersForView = acceptedPassengers.Select(x => x.ToCarpoolUserViewModel()).ToList();
            //}

            var carpoolOfferForDetailView = new CarpoolDetailsViewModel();
            carpoolOfferForDetailView.CarpoolOffer = carpoolOfferForView;
            carpoolOfferForDetailView.RequestingPassengers = requestingPassengersForView;
            carpoolOfferForDetailView.AcceptedPassengers = acceptedPassengersForView;

            return View(carpoolOfferForDetailView);
        }


    }
}
