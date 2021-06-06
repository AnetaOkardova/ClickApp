using ClickApp.Models;
using ClickApp.Serivices.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Serivices.Interfaces
{
    public interface IOffersService
    {
        List<Offer> GetAllPublicWithFilter(string title, bool isProffesional);
        void Create(Offer offer);
    }
}
