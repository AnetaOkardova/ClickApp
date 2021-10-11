using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface ICarpoolPassengerRequestsService
    {
        StatusModel Create(CarpoolPassengerRequest passengerRequest);
        bool CheckIfRequestExists(string passengerId, int carpoolOfferId);
        CarpoolPassengerRequest GetValidPassengerRequest(string passengerId, int carpoolOfferId);
        StatusModel CancelRequest(string passengerId, int carpoolOfferId);
        List<CarpoolPassengerRequest> GetAllValidByOfferId(int id);
        StatusModel Update(CarpoolPassengerRequest carpoolRequestToApprove);
    }
}
