using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Serivices.Interfaces
{
    public interface IOffersService
    {
        List<Offer> GetAllPublicWithFilter(string title);
    }
}
