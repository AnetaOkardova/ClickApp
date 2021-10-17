using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class CreateJournalEntryViewModel
    {
        public string UserId { get; set; }
        public string Theme { get; set; }
        public string? Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Public { get; set; }
    }
}
