using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface ICarpoolOfferService
    {
        List<CarpoolOffer> GetAllCarpoolOffers();
        CarpoolOffer GetById(int id);
        StatusModel Update(CarpoolOffer carpoolOffer);
    }
}
