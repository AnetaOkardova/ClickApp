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

        public CarpoolPassengerRequestsService(ICarpoolPassengerRequestsRepository carpoolPassengerRequestsRepository)
        {
            _carpoolPassengerRequestsRepository = carpoolPassengerRequestsRepository;
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
    }
}
