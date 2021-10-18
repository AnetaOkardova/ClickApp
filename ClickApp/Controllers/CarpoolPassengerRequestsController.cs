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
    public class CarpoolPassengerRequestsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICarpoolOfferService _carpoolOfferService;
        private readonly ICarpoolPassengerRequestsService _carpoolPassengerRequestsService;
        private readonly IMessagesService _messagesService;


        public CarpoolPassengerRequestsController(UserManager<ApplicationUser> userManager, ICarpoolPassengerRequestsService carpoolPassengerRequestsService, ICarpoolOfferService carpoolOfferService, IMessagesService messagesService)
        {
            _userManager = userManager;
            _carpoolOfferService = carpoolOfferService;
            _carpoolPassengerRequestsService = carpoolPassengerRequestsService;
            _messagesService = messagesService;
        }
        public IActionResult CreateRequest(int requestedSeats, string passengerId, int carpoolOfferId)
        {
            var offer = _carpoolOfferService.GetById(carpoolOfferId);

            if (offer == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"There are no active carpool offers with ID {carpoolOfferId}." });
            }
            if (requestedSeats == 0)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"To make a request you need to specify how many seats are you requesting. Please try again." });
            }
            if (passengerId == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"Opps. Something went wrong. Please try again later." });
            }
            var passengerRequest = new CarpoolPassengerRequest();
            passengerRequest.CarpoolOfferId = carpoolOfferId;
            passengerRequest.RequestingPassengerId = passengerId;
            passengerRequest.RequestedSeats = requestedSeats;
            passengerRequest.AcceptedStatus = false;
            passengerRequest.DateRequested = DateTime.Now;
            passengerRequest.Valid = true;

            var response = _carpoolPassengerRequestsService.Create(passengerRequest);

            _messagesService.CreateMessage(passengerId, offer.DriverId, "I write to inform you that I requested to carpool with you. Please advise if you accept this request. Thank you.");
            return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = response.Message });
        }
        public IActionResult CancelRequest(string passengerId, int carpoolOfferId, string userId)
        {
            if (passengerId == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You have no authorization to cancel these requests." });
            }
            if (carpoolOfferId == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"There is no carpool offer selected. Please try again later." });
            }
            if (userId == null)
            {
                return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You have no authorization to cancel these requests." });
            }

            var response = _carpoolPassengerRequestsService.CancelRequest(passengerId, carpoolOfferId);
            if (response.IsSuccessful == true)
            {
                _messagesService.CreateMessage(userId, passengerId, "Carpool request canceled.");
                return RedirectToAction("Overview", "CarpoolOffer", new { SuccessMessage = response.Message });
            }
            return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = response.Message });
        }
    }
}
