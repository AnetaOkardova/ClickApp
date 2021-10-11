using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface ICarpoolPassengerAcceptancesService
    {
        List<CarpoolPassengerAcceptance> GetAllValidByOfferId(int id);
        StatusModel CreatePassengerAcceptance(CarpoolPassengerAcceptance passengerAcceptance);
        StatusModel Update(CarpoolPassengerAcceptance carpoolAcceptanceToCancel);
    }
}
