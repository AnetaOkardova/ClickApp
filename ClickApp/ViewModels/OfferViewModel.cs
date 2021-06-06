using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsProfessional { get; set; }
        [Required]
        public bool IsPublic { get; set; }

        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime ValidUntil { get; set; }
        [Required]
        public string UserId { get; set; }
        public string ImportantNote { get; set; }
    }
}
