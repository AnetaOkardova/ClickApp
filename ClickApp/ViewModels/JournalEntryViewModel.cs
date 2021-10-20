using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class JournalEntryViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string? Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Theme { get; set; }
        public bool Public { get; set; }
    }
}
