using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.ViewModels
{
    public class JournalViewModel
    {
        public string UserId { get; set; }
        public List<NoteViewModel> Notes { get; set; } = new List<NoteViewModel>();
        public List<JournalEntryViewModel> JournalEntriesToShow { get; set; } = new List<JournalEntryViewModel>();
        public List<JournalThemeViewModel> Themes { get; set; } = new List<JournalThemeViewModel>();
        public JournalEntryViewModel OpenedJournalEntry { get; set; } = new JournalEntryViewModel();
        public JournalThemeViewModel ThemeSelected { get; set; } = new JournalThemeViewModel();

        public bool AddJournalEntry { get; set; } = false;
        public bool AddTheme { get; set; } = false;
        public bool AddEntry{ get; set; } = false;
        public bool EditEntry { get; set; } = false;
        //public bool DeleteEntry { get; set; } = false;
        //public bool DeleteNote { get; set; } = false;
    }
}
