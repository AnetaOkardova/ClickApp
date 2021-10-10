using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Models
{
    public class CarpoolPassengerRequest
    {
        public int Id { get; set; }
        public int CarpoolOfferId { get; set; }
        public string RequestingPassengerId { get; set; }
        public int RequestedSeats { get; set; }
        public DateTime DateRequested { get; set; }
    }

}
