using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services
{
    public class OffersService : IOffersService
    {
        private readonly IOffersRepository _offersRepository;

        public OffersService(IOffersRepository offersRepository)
        {
            _offersRepository = offersRepository;
        }
        [Authorize]
        public void Create(Offer offer)
        {
            _offersRepository.Create(offer);
        }

        public List<Offer> GetAllPublicWithFilter(string title, bool isProffesional)
        {
            return _offersRepository.GetAllPublicWithFilter(title, isProffesional);
        }
    }
}
