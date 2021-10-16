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
        private readonly ICarpoolPassengerAcceptancesService _carpoolPassengerAcceptancesService;
        private readonly ICarpoolPassengerRequestsService _carpoolPassengerRequestsService;

        public CarpoolOfferController(UserManager<ApplicationUser> userManager, ICarpoolOfferService carpoolOfferService, ICarpoolPassengerAcceptancesService carpoolPassengerAcceptancesService, ICarpoolPassengerRequestsService carpoolPassengerRequestsService)
        {
            _userManager = userManager;
            _carpoolOfferService = carpoolOfferService;
            _carpoolPassengerAcceptancesService = carpoolPassengerAcceptancesService;
            _carpoolPassengerRequestsService = carpoolPassengerRequestsService;

        }

        public IActionResult Overview(string successMessage, string errorMessage, string leavingLocationSearch)
        {
            if (successMessage != null)
            {
                ViewBag.SuccessMessage = successMessage;
            }
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            var carpoolOffers = new List<CarpoolOffer>();

            if (leavingLocationSearch != null && leavingLocationSearch != "")
            {
                carpoolOffers = _carpoolOfferService.GetAllCarpoolOffersByLeavingLocation(leavingLocationSearch);
            }
            else
            {
                carpoolOffers = _carpoolOfferService.GetAllCarpoolOffers();
            }

            var activeCarpoolOffers = carpoolOffers.Where(x => x.SeatsAvailable > 0).ToList();

            var viewCarpoolOffers = activeCarpoolOffers.Select(x => x.ToCarpoolOfferViewModel()).ToList();

            return View(viewCarpoolOffers);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var carpoolOffer = new CreateCarpoolOfferViewModel();
            return View(carpoolOffer);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarpoolOfferViewModel createCarpoolOfferViewModel)
        {
            if (createCarpoolOfferViewModel.LeavingFrom != null && createCarpoolOfferViewModel.ArrivingAt != null && createCarpoolOfferViewModel.LeavingHour != null && createCarpoolOfferViewModel.LeavingMinutes != null && createCarpoolOfferViewModel.SeatsAvailable != 0)
            {
                var user = User;
                var userFromDb = await _userManager.FindByNameAsync(user.Identity.Name);

                var carpoolOffer = createCarpoolOfferViewModel.ToModel();
                carpoolOffer.DateCreated = DateTime.Now;
                carpoolOffer.DriverId = userFromDb.Id;
                carpoolOffer.RequestingPassengers = new List<CarpoolPassengerRequest>();
                carpoolOffer.AcceptedPassengers = new List<CarpoolPassengerAcceptance>();
                carpoolOffer.Driver = userFromDb;
                int carpoolOfferId = _carpoolOfferService.Create(carpoolOffer);
                return RedirectToAction("Details", new { id = carpoolOfferId });
            }
            else
            {
                return View(createCarpoolOfferViewModel);
            }
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
                    return RedirectToAction("Details", new { id = carpoolOffer.Id, successMessage = $"The carpool offer with ID {carpoolOffer.Id} has been successfully updated." });
                }
                else
                {
                    return RedirectToAction("Details", new { id = carpoolOffer.Id, ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(carpoolOfferViewModel);
            }
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var response = _carpoolOfferService.Delete(id);

            if (response.IsSuccessful)
            {
                return RedirectToAction("Overview", new { successMessage = response.Message });
            }
            return RedirectToAction("Overview", new { errorMessage = response.Message });
        }
        [Authorize]
        public async Task<IActionResult> Details(int id, string successMessage, string errorMessage)
        {
            if (successMessage != null)
            {
                ViewBag.SuccessMessage = successMessage;
            }
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
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
            var requestingPassengersForView = new List<CarpoolSeatsDetailsModel>();
            var passengerRequestsForOffer = _carpoolPassengerRequestsService.GetAllValidByOfferId(id);

            if (passengerRequestsForOffer.Count != 0 && passengerRequestsForOffer != null)
            {
                foreach (var passengerRequest in passengerRequestsForOffer)
                {
                    var passenger = await _userManager.FindByIdAsync(passengerRequest.RequestingPassengerId);
                    var requestedSeats = passengerRequest.RequestedSeats;

                    var requestingPassengerWithSeats = new CarpoolSeatsDetailsModel();
                    requestingPassengerWithSeats.Passenger = passenger.ToCarpoolUserViewModel();
                    requestingPassengerWithSeats.Seats = requestedSeats;

                    requestingPassengersForView.Add(requestingPassengerWithSeats);
                }
            }

            var acceptedPassengersForView = new List<CarpoolSeatsDetailsModel>();
            var acceptedPassengersForOffer = _carpoolPassengerAcceptancesService.GetAllValidByOfferId(id);

            if (acceptedPassengersForOffer.Count != 0 && acceptedPassengersForOffer != null)
            {
                foreach (var acceptedPassenger in acceptedPassengersForOffer)
                {
                    var passenger = await _userManager.FindByIdAsync(acceptedPassenger.AcceptedPassengerId);
                    var reservedSeats = acceptedPassenger.ReservedSeats;

                    var acceptedPassengerWithSeats = new CarpoolSeatsDetailsModel();
                    acceptedPassengerWithSeats.Passenger = passenger.ToCarpoolUserViewModel();
                    acceptedPassengerWithSeats.Seats = reservedSeats;

                    acceptedPassengersForView.Add(acceptedPassengerWithSeats);
                }
            }
            var carpoolOfferForDetailView = new CarpoolDetailsViewModel();
            carpoolOfferForDetailView.CarpoolOffer = carpoolOfferForView;
            carpoolOfferForDetailView.RequestingPassengers = requestingPassengersForView;
            carpoolOfferForDetailView.AcceptedPassengers = acceptedPassengersForView;
            return View(carpoolOfferForDetailView);
        }
    }
}
