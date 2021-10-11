using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CarpoolDetailsViewModel
    {
        public CarpoolOfferViewModel CarpoolOffer { get; set; }
        public List<CarpoolSeatsDetailsModel> RequestingPassengers { get; set; }
        public List<CarpoolSeatsDetailsModel> AcceptedPassengers { get; set; }

    }
}
