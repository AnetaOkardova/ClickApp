using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.DtoModels;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Services
{
    public class JournalThemesService : IJournalThemesService
    {
        private readonly IJournalThemesRepository _journalThemesRepository;
            private readonly IJournalEntriesRepository _journalEntriesRepository;

        public JournalThemesService(IJournalThemesRepository journalThemesRepository, IJournalEntriesRepository journalEntriesRepository)
        {
            _journalThemesRepository = journalThemesRepository;
            _journalEntriesRepository = journalEntriesRepository;
        }

        public bool CheckIfExists(string userId, string theme)
        {
            var exists = false;
            var allJournalEntries = _journalThemesRepository.GetAll();
            var journalThemeFromDb = allJournalEntries.FirstOrDefault(x => x.UserId == userId && x.Name == theme);
            if(journalThemeFromDb != null)
            {
                exists = true;
            }
            return exists;
        }

        public StatusModel Create(ApplicationUser user, string theme)
        {
            var response = new StatusModel();
            var userJournalThemes = user.JournalThemes;
            bool exists = CheckIfExists(user.Id, theme);
            if (exists)
            {
                response.IsSuccessful = false;
                response.Message = $"You already have a {theme} theme";
                return response;
            }

            var newJournalTheme = new JournalTheme();
            newJournalTheme.User = user;
            newJournalTheme.UserId = user.Id;
            newJournalTheme.Name = theme;

            _journalThemesRepository.Create(newJournalTheme);
            response.Message = $"You successfully created the theme: {theme}";

            return response;
        }

        public List<JournalTheme> GetAllByUserId(string userId)
        {
            return _journalThemesRepository.GetAll().Where(x => x.UserId == userId).ToList();
        }

        public List<JournalTheme> GetAllByName(string themeName)
        {
            return _journalThemesRepository.GetAll().Where(x => x.Name == themeName).ToList();
        }

        public void Delete(string userId,string theme)
        {
            var allJournalEntriesForJournalTheme = _journalEntriesRepository.GetAll().Where(x => x.UserId == userId && x.Theme == theme).ToList();
            if (allJournalEntriesForJournalTheme.Count != 0 && allJournalEntriesForJournalTheme != null)
            {
                foreach (var journalEntry in allJournalEntriesForJournalTheme)
                {
                    _journalEntriesRepository.Delete(journalEntry);
                }
            }
            var allJournalThemes = GetAllByName(theme);
            var userJournalTheme = allJournalThemes.FirstOrDefault(x => x.UserId == userId);
            if (userJournalTheme != null)
            {
                _journalThemesRepository.Delete(userJournalTheme);
            }
        }
    }
}
