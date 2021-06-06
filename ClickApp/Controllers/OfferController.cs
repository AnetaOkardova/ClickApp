using ClickApp.Mappings;
using ClickApp.Serivices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOffersService _offersService;

        public OfferController(IOffersService offersService)
        {
            _offersService = offersService;
        }
        public IActionResult Overview(string offerTitle)
        {
            return View();
        }
        public IActionResult GetAllWithFilter(string offerTitle, bool isProffesional)
        {
            var offers = _offersService.GetAllPublicWithFilter(offerTitle, isProffesional);
            if (offers.Count == 0)
            {
                ViewBag.EmptyListMessage = "Currently there are no offers. Try again later.";
            }
            var viewOffers = offers.Select(x => x.ToOfferViewModel()).ToList();

            return View(viewOffers);
        }

    }
}
