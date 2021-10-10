using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
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
    }
}
