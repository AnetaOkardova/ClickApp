using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsValid { get; set; }
        public string Content { get; set; }
    }
}
