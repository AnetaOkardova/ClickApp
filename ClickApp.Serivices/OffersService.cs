using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Serivices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Serivices
{
    public class OffersService : IOffersService
    {
        private readonly IOffersRepository _offersRepository;

        public OffersService(IOffersRepository offersRepository)
        {
            _offersRepository = offersRepository;
        }

        public List<Offer> GetAllPublicWithFilter(string title, bool isProffesional)
        {
            return _offersRepository.GetAllPublicWithFilter(title, isProffesional);
        }
    }
}
