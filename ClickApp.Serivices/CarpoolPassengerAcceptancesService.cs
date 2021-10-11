using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Services
{
    public class CarpoolPassengerAcceptancesService : ICarpoolPassengerAcceptancesService
    {
        private readonly ICarpoolPassengerAcceptancesRepository _carpoolPassengerAcceptancesRepository;
        private readonly ICarpoolOffersRepository _carpoolOffersRepository;


        public CarpoolPassengerAcceptancesService(ICarpoolPassengerAcceptancesRepository carpoolPassengerAcceptancesRepository, ICarpoolOffersRepository carpoolOffersRepository)
        {
            _carpoolPassengerAcceptancesRepository = carpoolPassengerAcceptancesRepository;
            _carpoolOffersRepository = carpoolOffersRepository;
        }


        public StatusModel CreatePassengerAcceptance(CarpoolPassengerAcceptance passengerAcceptance)
        {
            var response = new StatusModel();

            var checkIfExists = CheckIfRequestExists(passengerAcceptance.AcceptedPassengerId, passengerAcceptance.CarpoolOfferId);

            if (checkIfExists)
            {
                response.IsSuccessful = false;
                response.Message = $"You have already approved seats for this offer.";
                return response;
            }
            _carpoolPassengerAcceptancesRepository.Create(passengerAcceptance);
            return response;
        }
        public bool CheckIfRequestExists(string passengerId, int carpoolOfferId)
        {
            return _carpoolPassengerAcceptancesRepository.GetAll().Any((x => x.CarpoolOfferId == carpoolOfferId && x.AcceptedPassengerId == passengerId && x.Valid == true));
        }
        public List<CarpoolPassengerAcceptance> GetAllValidByOfferId(int id)
        {
            var offers = _carpoolPassengerAcceptancesRepository.GetAll();
            var allAcceptancesForAnOffer = offers.Where(x => x.CarpoolOfferId == id && x.Valid == true).ToList();
            if (allAcceptancesForAnOffer.Count == 0 || allAcceptancesForAnOffer == null)
            {
                return new List<CarpoolPassengerAcceptance>();
            }
            return allAcceptancesForAnOffer;
        }
        public StatusModel Update(CarpoolPassengerAcceptance carpoolAcceptanceToCancel)
        {
            var response = new StatusModel();
            var offer = _carpoolOffersRepository.GetById(carpoolAcceptanceToCancel.CarpoolOfferId);

            if (offer == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The carpool offer with ID {carpoolAcceptanceToCancel.CarpoolOfferId} is not found.";
            }
            else
            {
                _carpoolPassengerAcceptancesRepository.Update(carpoolAcceptanceToCancel);

                offer.SeatsAvailable += carpoolAcceptanceToCancel.ReservedSeats;
                _carpoolOffersRepository.Update(offer);

                response.Message = $"The carpool passenger has been canceled.";
            }
            return response;
        }
    }
}
