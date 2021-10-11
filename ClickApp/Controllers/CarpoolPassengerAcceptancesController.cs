using ClickApp.Models;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class CarpoolPassengerAcceptancesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICarpoolOfferService _carpoolOfferService;
        private readonly ICarpoolPassengerAcceptancesService _carpoolPassengerAcceptancesService;
        private readonly ICarpoolPassengerRequestsService _carpoolPassengerRequestsService;


        public CarpoolPassengerAcceptancesController(UserManager<ApplicationUser> userManager, ICarpoolPassengerAcceptancesService carpoolPassengerAcceptancesService, ICarpoolOfferService carpoolOfferService, ICarpoolPassengerRequestsService carpoolPassengerRequestsService)
        {
            _userManager = userManager;
            _carpoolOfferService = carpoolOfferService;
            _carpoolPassengerAcceptancesService = carpoolPassengerAcceptancesService;
            _carpoolPassengerRequestsService = carpoolPassengerRequestsService;
        }
        public IActionResult CreatePassengerAcceptance(string userId, int carpoolOfferId, string passengerId)
        {
            var offer = _carpoolOfferService.GetById(carpoolOfferId);
            if (offer == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"There are no active carpool offers with ID {carpoolOfferId}." });
            }

            if (offer.DriverId != userId)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You are not authorized to accept passengers." });
            }

            var carpoolPassengerRequest = _carpoolPassengerRequestsService.GetAllValidByOfferId(offer.Id);
            if (carpoolPassengerRequest == null || carpoolPassengerRequest.Count == 0)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You have no active carpool offers with ID {carpoolOfferId}." });
            }
            var carpoolRequestToApprove = carpoolPassengerRequest.FirstOrDefault(x => x.RequestingPassengerId == passengerId && x.Valid == true);
            if (carpoolRequestToApprove == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You have no request for carpool offer with ID {carpoolOfferId}." });
            }

            carpoolRequestToApprove.Valid = false;
            carpoolRequestToApprove.AcceptedStatus = true;

            var response = _carpoolPassengerRequestsService.Update(carpoolRequestToApprove);
            if (!response.IsSuccessful)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = response.Message });
            }
            
            var passengerAcceptance = new CarpoolPassengerAcceptance();
            passengerAcceptance.AcceptedPassengerId = carpoolRequestToApprove.RequestingPassengerId;
            passengerAcceptance.CarpoolOfferId = carpoolRequestToApprove.CarpoolOfferId;
            passengerAcceptance.DateAccepted = DateTime.Now;
            passengerAcceptance.ReservedSeats = carpoolRequestToApprove.RequestedSeats;
            passengerAcceptance.Valid = true;
            passengerAcceptance.AcceptedStatus = true;

            var responseForCreatingAcceptance = _carpoolPassengerAcceptancesService.CreatePassengerAcceptance(passengerAcceptance);
            if (responseForCreatingAcceptance.IsSuccessful)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { SuccessMessage = $"The passanger has been successfully approved." });
            }
            else
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = response.Message });
            }
        }

        public IActionResult CancelAcceptance(string userId, int carpoolOfferId, string passengerId)
        {
            var offer = _carpoolOfferService.GetById(carpoolOfferId);
            if (offer == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"There are no active carpool offers with ID {carpoolOfferId}." });
            }

            if (offer.DriverId != userId)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You are not authorized to accept passengers." });
            }

            var carpoolAcceptances = _carpoolPassengerAcceptancesService.GetAllValidByOfferId(offer.Id);
            if (carpoolAcceptances == null || carpoolAcceptances.Count == 0)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You have no active passenger with ID {passengerId}." });
            }
            var carpoolAcceptanceToCancel = carpoolAcceptances.FirstOrDefault(x => x.AcceptedPassengerId == passengerId && x.Valid == true);
            if (carpoolAcceptances == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You have no acceptance to cancel for carpool offer with ID ${carpoolOfferId}." });
            }

            carpoolAcceptanceToCancel.Valid = false;
            carpoolAcceptanceToCancel.AcceptedStatus = false;

            var response = _carpoolPassengerAcceptancesService.Update(carpoolAcceptanceToCancel);
            if (!response.IsSuccessful)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = response.Message });
            }
            return RedirectToAction("Overview", "CarpoolOffer", new { SuccessMessage = $"The passanger acceptance has been canceled." });
        }
    }
}
