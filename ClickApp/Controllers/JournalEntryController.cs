using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class JournalEntryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJournalEntriesService _journalEntriesService;
        private readonly INotesService _notesService;

        public JournalEntryController(UserManager<ApplicationUser> userManager, IJournalEntriesService journalEntriesService, INotesService notesService)
        {
            _userManager = userManager;
            _journalEntriesService = journalEntriesService;
            _notesService = notesService;

        }
        [Authorize]
        public async Task<IActionResult> Overview(string userId, string theme, int entryId, bool addEntry, bool addTheme, bool editEntry)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var journalView = new JournalViewModel();
            journalView.UserId = user.Id;

            if (entryId !=0)
            {
                var openedJournalEntry = _journalEntriesService.GetById(entryId);
                if(openedJournalEntry == null)
                {
                    ViewBag.ErrorMessage = $"There is no journal entry with ID {entryId}.";
                    return RedirectToAction("Overview", new { UserId = userId, Theme = theme });
                }
                if (openedJournalEntry.UserId != user.Id)
                {
                    ViewBag.ErrorMessage = "This entry is not yours to manage.";
                    return RedirectToAction("Overview", new { UserId = userId, Theme = theme });
                }
                journalView.OpenedJournalEntry = openedJournalEntry.ToJournalEntryViewModel();
                if (editEntry)
                {
                    journalView.EditEntry = true;
                }
            }
            if (addEntry)
            {
                journalView.AddJournalEntry = true;
            }
            if (addTheme)
            {
                journalView.AddTheme = true;
            }

            var allUserNotes = _notesService.GetAllByUserId(user.Id);
            if(allUserNotes != null && allUserNotes.Count !=0)
            {
                journalView.Notes = allUserNotes.Select(x=>x.ToNoteViewModel()).ToList();
            }

            var allUserJournalEntries = _journalEntriesService.GetByUserId(user.Id);
            if (allUserJournalEntries != null && allUserJournalEntries.Count!=0)
            {
                journalView.JournalEntriesToShow = allUserJournalEntries.Select(x => x.ToJournalEntryViewModel()).ToList();
                var userThemes = new List<string>();

                foreach (var journalEntry in allUserJournalEntries)
                {
                    var themeValue = journalEntry.Theme;

                    if (!userThemes.Contains(themeValue))
                    {
                        userThemes.Add(themeValue);
                    }
                }
                journalView.Themes = userThemes;
            }
            
            return View(journalView);
        }

    }
}
