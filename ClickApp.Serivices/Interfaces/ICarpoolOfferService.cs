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
        CarpoolOffer GetById(int carpoolOfferId);
        StatusModel Update(CarpoolOffer carpoolOffer);
        List<CarpoolOffer> GetAllCarpoolOffersByUserId(string id);
        int Create(CarpoolOffer carpoolOffer);
        StatusModel Delete(int carpoolOfferId);
        List<CarpoolOffer> GetAllCarpoolOffersByLeavingLocation(string leavingLocation);
    }
}
