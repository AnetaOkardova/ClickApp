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
    public class CarpoolOfferService : ICarpoolOfferService
    {
        private readonly ICarpoolOffersRepository _carpoolOffersRepository;

        public CarpoolOfferService(ICarpoolOffersRepository carpoolOffersRepository)
        {
            _carpoolOffersRepository = carpoolOffersRepository;
        }


        public List<CarpoolOffer> GetAllCarpoolOffers()
        {
            return _carpoolOffersRepository.GetAll().ToList();
        }

        public CarpoolOffer GetById(int id)
        {
            return _carpoolOffersRepository.GetById(id);
        }

        public StatusModel Update(CarpoolOffer carpoolOffer)
        {
            var response = new StatusModel();
            var carpoolOfferFromDB = _carpoolOffersRepository.GetById(carpoolOffer.Id);
            if (carpoolOfferFromDB == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The carpool offer with ID {carpoolOffer.Id} is not found.";
            }
            else
            {
                carpoolOfferFromDB.LeavingFrom = carpoolOffer.LeavingFrom;
                carpoolOfferFromDB.LeavingHour = carpoolOffer.LeavingHour;
                carpoolOfferFromDB.LeavingMinutes = carpoolOffer.LeavingMinutes;
                carpoolOfferFromDB.LeavingNote = carpoolOffer.LeavingNote;
                carpoolOfferFromDB.RequestingPassengers = carpoolOffer.RequestingPassengers;
                carpoolOfferFromDB.ReturnAt = carpoolOffer.ReturnAt;
                carpoolOfferFromDB.ReturnFrom = carpoolOffer.ReturnFrom;
                carpoolOfferFromDB.ReturnHour = carpoolOffer.ReturnHour;
                carpoolOfferFromDB.ReturnMinutes = carpoolOffer.ReturnMinutes;
                carpoolOfferFromDB.ReturnNote = carpoolOffer.ReturnNote;
                carpoolOfferFromDB.ReturnSeatsAvailable = carpoolOffer.ReturnSeatsAvailable;
                carpoolOfferFromDB.SeatsAvailable = carpoolOffer.SeatsAvailable;
                carpoolOfferFromDB.AcceptedPassengers = carpoolOffer.AcceptedPassengers;
                carpoolOfferFromDB.ArrivingAt = carpoolOffer.ArrivingAt;

                _carpoolOffersRepository.Update(carpoolOfferFromDB);
                response.Message = $"The carpool offer with ID {carpoolOfferFromDB.Id} has been successfully updated.";
            }
            return response;
        }
    }
}
