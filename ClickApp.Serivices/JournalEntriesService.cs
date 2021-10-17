using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
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
