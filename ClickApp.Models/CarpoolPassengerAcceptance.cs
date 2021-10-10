using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Models
{
    public class CarpoolPassengerAcceptance
    {
        public int Id { get; set; }
        public int CarpoolOfferId { get; set; }
        public string AcceptedPassengerId { get; set; }
        public int ReservedSeats { get; set; }
        public DateTime DateAccepted { get; set; }
    }

}
