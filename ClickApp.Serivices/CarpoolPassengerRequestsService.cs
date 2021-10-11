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
    public class CarpoolPassengerRequestsService : ICarpoolPassengerRequestsService
    {
        private readonly ICarpoolPassengerRequestsRepository _carpoolPassengerRequestsRepository;
        private readonly ICarpoolOffersRepository _carpoolOffersRepository;


        public CarpoolPassengerRequestsService(ICarpoolPassengerRequestsRepository carpoolPassengerRequestsRepository, ICarpoolOffersRepository carpoolOffersRepository)
        {
            _carpoolPassengerRequestsRepository = carpoolPassengerRequestsRepository;
            _carpoolOffersRepository = carpoolOffersRepository;
        }

        public StatusModel Create(CarpoolPassengerRequest passengerRequest)
        {
            var response = new StatusModel();

            var checkIfExists = CheckIfRequestExists(passengerRequest.RequestingPassengerId, passengerRequest.CarpoolOfferId);

            if (checkIfExists)
            {
                response.IsSuccessful = false;
                response.Message = $"You have already requested seats for this offer.";
                return response;
            }

            _carpoolPassengerRequestsRepository.Create(passengerRequest);
            return response;
        }

        public bool CheckIfRequestExists(string passengerId, int carpoolOfferId)
        {
            return _carpoolPassengerRequestsRepository.GetAll().Any((x => x.CarpoolOfferId == carpoolOfferId && x.RequestingPassengerId == passengerId && x.Valid == true));
        }
        public CarpoolPassengerRequest GetValidPassengerRequest(string passengerId, int carpoolOfferId)
        {
            return _carpoolPassengerRequestsRepository.GetAll().FirstOrDefault((x => x.CarpoolOfferId == carpoolOfferId && x.RequestingPassengerId == passengerId && x.Valid == true));
        }
        public StatusModel CancelRequest(string passengerId, int carpoolOfferId)
        {
            var response = new StatusModel();

            var passengerRequestFromDB = GetValidPassengerRequest(passengerId, carpoolOfferId);

            if (passengerRequestFromDB == null)
            {
                response.IsSuccessful = false;
                response.Message = $"You have no requested seats for this offer to cancel.";
                return response;
            }
            passengerRequestFromDB.Valid = false;

            _carpoolPassengerRequestsRepository.Update(passengerRequestFromDB);

            return response;
        }

        public List<CarpoolPassengerRequest> GetAllValidByOfferId(int id)
        {
            var offers = _carpoolPassengerRequestsRepository.GetAll();
            var allRequestsForAnOffer = offers.Where(x => x.CarpoolOfferId == id && x.Valid == true).ToList();
            if (allRequestsForAnOffer.Count == 0 || allRequestsForAnOffer == null)
            {
                return new List<CarpoolPassengerRequest>();
            }
            return allRequestsForAnOffer;
        }

        public StatusModel Update(CarpoolPassengerRequest carpoolRequestToApprove)
        {
            var response = new StatusModel();
            var offer = _carpoolOffersRepository.GetById(carpoolRequestToApprove.CarpoolOfferId);

            if (offer == null)
            {
                response.IsSuccessful = false;
                response.Message = $"The carpool offer with ID {carpoolRequestToApprove.CarpoolOfferId} is not found.";
            }
            else
            {
                _carpoolPassengerRequestsRepository.Update(carpoolRequestToApprove);

                offer.SeatsAvailable -= carpoolRequestToApprove.RequestedSeats;
                _carpoolOffersRepository.Update(offer);

                response.Message = $"The carpool passenger request has been accepted.";
            }
            return response;
        }
    }
}
