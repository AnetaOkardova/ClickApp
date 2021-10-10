using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class CarpoolOffer
    {
        public int Id { get; set; }
        [Required]
        public ApplicationUser Driver { get; set; }
        [Required]
        public string LeavingFrom { get; set; }
        [Required]
        public string ArrivingAt { get; set; }
        [Required]
        public int LeavingHour { get; set; }
        [Required]
        public int LeavingMinutes { get; set; }
        [Required]
        public int SeatsAvailable { get; set; }
        [Required]
        public string LeavingNote { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        public string? ReturnFrom { get; set; }
        public string? ReturnAt { get; set; }
        public int? ReturnHour { get; set; }
        public int? ReturnMinutes { get; set; }
        public int? ReturnSeatsAvailable { get; set; }
        public string? ReturnNote { get; set; }

        public List<CarpoolPassengerRequest>? RequestingPassengers { get; set; }
        public List<CarpoolPassengerAcceptance>? AcceptedPassengers { get; set; }
    }

}
