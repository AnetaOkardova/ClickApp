using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IOffersService
    {
        List<Offer> GetAllPublicWithFilter(string title, bool isProffesional);
        void Create(Offer offer);
    }
}
