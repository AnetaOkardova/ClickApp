using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CarpoolDetailsViewModel
    {
        public CarpoolOfferViewModel CarpoolOffer { get; set; }
        public List<CarpoolUserViewModel> RequestingPassengers { get; set; }
        public List<CarpoolUserViewModel> AcceptedPassengers { get; set; }

    }
}
