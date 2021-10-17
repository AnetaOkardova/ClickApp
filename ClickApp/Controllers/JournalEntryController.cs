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
        private readonly IJournalThemesService _journalThemesService;

        private readonly INotesService _notesService;

        public JournalEntryController(UserManager<ApplicationUser> userManager, IJournalEntriesService journalEntriesService, INotesService notesService, IJournalThemesService journalThemesService)
        {
            _userManager = userManager;
            _journalEntriesService = journalEntriesService;
            _notesService = notesService;
            _journalThemesService = journalThemesService;

        }
        [Authorize]
        public async Task<IActionResult> Overview(string userId, string theme, int entryId, bool addEntry, bool addTheme, bool editEntry, string errorMessage, string successMessage)
        {
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            if (successMessage != null)
            {
                ViewBag.SuccessMessage = successMessage;
            }
            var user = await _userManager.FindByIdAsync(userId);
            var journalView = new JournalViewModel();

            journalView.UserId = user.Id;

            if (entryId != 0)
            {
                var openedJournalEntry = _journalEntriesService.GetById(entryId);
                if (openedJournalEntry == null)
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
            if (allUserNotes != null && allUserNotes.Count != 0)
            {
                journalView.Notes = allUserNotes.Select(x => x.ToNoteViewModel()).ToList();
            }
            var allThemes = _journalThemesService.GetAllByUserId(user.Id);
            if (allThemes.Count != 0)
            {
                journalView.Themes = allThemes.Select(x => x.ToJournalThemeViewModel()).ToList(); ;
            }

            var allUserJournalEntries = _journalEntriesService.GetByUserId(user.Id);
            if (allUserJournalEntries != null && allUserJournalEntries.Count != 0)
            {
                var allUserJournalEntriesPerTheme = allUserJournalEntries.Where(x => x.Theme == theme).ToList();
                journalView.JournalEntriesToShow = allUserJournalEntriesPerTheme.Select(x => x.ToJournalEntryViewModel()).ToList();
            }
            if (theme != null && theme != "")
            {
                var allThemesWithGivenName = _journalThemesService.GetAllByName(theme);
                var themeSelected = allThemesWithGivenName.FirstOrDefault(x => x.Name == theme && x.UserId == user.Id);
                if (themeSelected != null)
                {
                    journalView.ThemeSelected = themeSelected.ToJournalThemeViewModel();
                }
            }
            return View(journalView);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTheme(CreateJournalThemeViewModel createJournalThemeViewModel)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(createJournalThemeViewModel.UserId);

                var response = _journalThemesService.Create(userFromDb, createJournalThemeViewModel.Theme);
                if (!response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { UserId = createJournalThemeViewModel.UserId, ErrorMessage = response.Message });
                }
                return RedirectToAction("Overview", new { UserId = createJournalThemeViewModel.UserId, SuccessMessage = response.Message });
            }
            else
            {
                return RedirectToAction("Overview", new { UserId = createJournalThemeViewModel.UserId, ErrorMessage = "You have to add a theme name to create a theme." });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateJournalEntry(CreateJournalEntryViewModel createJournalEntryViewModel)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(createJournalEntryViewModel.UserId);
                var journalEntry = createJournalEntryViewModel.ToModel();

                _journalEntriesService.Create(journalEntry);
            }
            return RedirectToAction("Overview", new { AddEntry = true, Theme = createJournalEntryViewModel.Theme, UserId = createJournalEntryViewModel.UserId });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNote(string userId, string theme, string content)
        {
            if (userId != null && content != null)
            {
                var note = new Note();
                note.UserId = userId;
                note.IsValid = true;
                note.Content = content;
                note.DateCreated = DateTime.Now;

                _notesService.Create(note);
            }
            return RedirectToAction("Overview", new { Theme = theme, UserId = userId });
        }
        [Authorize]
        public async Task<IActionResult> DeleteNote(string userId, string theme, int noteId)
        {
            bool findIfExtsts = _notesService.FindIfNoteExistsForUser(userId, noteId);

            if (findIfExtsts)
            {
                var response = _notesService.Delete(noteId);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { Theme = theme, UserId = userId });
                }
            }
            return RedirectToAction("Overview", new { Theme = theme, UserId = userId });
        }

        [Authorize]
        public async Task<IActionResult> DeleteTheme(string userId, string theme)
        {
            bool findIfExtsts = _journalThemesService.CheckIfExists(userId, theme);
            if (findIfExtsts)
            {
                _journalThemesService.Delete(userId, theme);
            }
            return RedirectToAction("Overview", new { Theme = theme, UserId = userId });
        }
    }
}
