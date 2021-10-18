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
    public class JournalEntriesService : IJournalEntriesService
    {
        private readonly IJournalEntriesRepository _journalEntriesRepository;

        public JournalEntriesService(IJournalEntriesRepository journalEntriesRepository)
        {
            _journalEntriesRepository = journalEntriesRepository;
        }

        public void Create(JournalEntry journalEntry)
        {
            var response = new StatusModel();
            journalEntry.DateCreated = DateTime.Now;
            _journalEntriesRepository.Create(journalEntry);
        }

        public StatusModel DeleteEntry(string userId, int entryId)
        {
            var response = new StatusModel();
            var jurnalEntry = _journalEntriesRepository.GetById(entryId);
            if (jurnalEntry != null && jurnalEntry.UserId == userId)
            {
                _journalEntriesRepository.Delete(jurnalEntry);
                response.Message = $"The journal entry with id {entryId} has been successfully deleted.";
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = $"The journal entry with id {entryId} is not yours to delete.";
            }
            return response;
        }

        public JournalEntry GetById(int entryId)
        {
            return _journalEntriesRepository.GetById(entryId);
        }

        public List<JournalEntry> GetByUserId(string id)
        {
            var allJournalEntries = _journalEntriesRepository.GetAll();
            var allUserEntries = allJournalEntries.Where(x => x.UserId == id).ToList();
            return allUserEntries;
        }
    }
}
