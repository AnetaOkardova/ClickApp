using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public bool IsValid { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
