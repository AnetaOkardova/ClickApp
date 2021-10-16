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

        public int Create(CarpoolOffer carpoolOffer)
        {
            _carpoolOffersRepository.Create(carpoolOffer);
            var carpoolOfferId = _carpoolOffersRepository.GetAll().FirstOrDefault(x => x.DateCreated == carpoolOffer.DateCreated && x.DriverId == carpoolOffer.DriverId).Id;
            return carpoolOfferId;
        }

        public StatusModel Delete(int carpoolOfferId)
        {
           var response = new StatusModel();
            var carpoolOffer = _carpoolOffersRepository.GetById(carpoolOfferId);
            if(carpoolOffer == null)
            {
                response.IsSuccessful = false;
                response.Message = $"There is no carpool offer with Id {carpoolOfferId}";
                return response;
            }
            _carpoolOffersRepository.Delete(carpoolOffer);
            response.Message = $"The carpool offer with id {carpoolOfferId} has been successfully deleted.";
            return response;
        }

        public List<CarpoolOffer> GetAllCarpoolOffers()
        {
            return _carpoolOffersRepository.GetAll().ToList();
        }

        public List<CarpoolOffer> GetAllCarpoolOffersByLeavingLocation(string leavingLocation)
        {
            var allCarpoolOffers = _carpoolOffersRepository.GetAll().ToList();
            var allCarpoolOffersForALeavingLocation = allCarpoolOffers.Where(x => x.LeavingFrom.ToLower().Contains(leavingLocation.ToLower())).ToList();
            return allCarpoolOffersForALeavingLocation;
        }

        public List<CarpoolOffer> GetAllCarpoolOffersByUserId(string id)
        {
            return _carpoolOffersRepository.GetAll().Where(x => x.DriverId == id).ToList();
        }

        public CarpoolOffer GetById(int carpoolOfferId)
        {
            return _carpoolOffersRepository.GetById(carpoolOfferId);
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
