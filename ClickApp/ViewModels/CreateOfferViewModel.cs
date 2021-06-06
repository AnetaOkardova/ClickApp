using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CreateOfferViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsProfessional { get; set; }
        [Required]
        public bool IsPublic { get; set; }

        public string ImageUrl { get; set; }
        public DateTime ValidUntil { get; set; }
        public string ImportantNote { get; set; }
    }
}
