using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface IOffersRepository
    {
        List<Offer> GetAllPublicWithFilter(string title, bool isProffesional);
        void Create(Offer offer);
        List<Offer> GetAllOffersForUser(string userId);
    }
}
