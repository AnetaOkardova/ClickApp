﻿using ClickApp.Models;
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
        public IActionResult CreatePaasengerAcceptance(int requestedSeats, string passengerId, int carpoolOfferId)
        {
            //var offer = _carpoolOfferService.GetById(carpoolOfferId);

            //if (offer == null)
            //{
            //    return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"There are no active carpool offers with ID {carpoolOfferId}." });
            //}
            //if (requestedSeats == 0)
            //{
            //    return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"To make a request you need to specify how many seats are you requesting. Please try again." });
            //}
            //if (passengerId == null)
            //{
            //    return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"Opps. Something went wrong. Please try again later." });
            //}
            //var passengerRequest = new CarpoolPassengerRequest();
            //passengerRequest.CarpoolOfferId = carpoolOfferId;
            //passengerRequest.RequestingPassengerId = passengerId;
            //passengerRequest.RequestedSeats = requestedSeats;
            //passengerRequest.AcceptedStatus = false;
            //passengerRequest.DateRequested = DateTime.Now;
            //passengerRequest.Valid = true;

            //var response = _carpoolPassengerRequestsService.Create(passengerRequest);
            //if(response.IsSuccessful == true)
            //{
            //    offer.SeatsAvailable -= requestedSeats;
            //    _carpoolOfferService.Update(offer);
            //}
            return RedirectToAction("Overview", "CarpoolOffer"/*,*/ /*new { ErrorMessage = response.Message }*/);
        }

        public IActionResult CancelRequest(string passengerId, int carpoolOfferId)
        {
            //if (passengerId == null)
            //{
            //    return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"You have no authorization to cancel these requests." });
            //}
            //if (carpoolOfferId == null)
            //{
            //    return RedirectToAction("Overview", "CarpoolOffer", new { ErrorMessage = $"There is no carpool offer selected. Please try again later." });
            //}

            //var response = _carpoolPassengerRequestsService.CancelRequest(passengerId, carpoolOfferId);
            //if (response.IsSuccessful == true)
            //{
            //    return RedirectToAction("Overview", "CarpoolOffer", new { SuccessMessage = response.Message });
            //}
            return RedirectToAction("Overview", "CarpoolOffer"/*,*/ /*new { ErrorMessage = response.Message }*/);
        }
    }
}
