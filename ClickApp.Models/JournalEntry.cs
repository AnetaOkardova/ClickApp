using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClickApp.Models
{
    public class JournalEntry
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string Content { get; set; }
        public string? Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [Required]
        public string Theme { get; set; }
        [Required]
        public bool Public { get; set; }
    }
}
