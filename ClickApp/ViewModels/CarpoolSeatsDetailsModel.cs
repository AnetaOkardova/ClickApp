using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CarpoolSeatsDetailsModel
    {
        public int Seats { get; set; }
        public CarpoolUserViewModel Passenger { get; set; }
    }
}
