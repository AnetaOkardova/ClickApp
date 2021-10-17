using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IJournalEntriesService
    {
        JournalEntry GetById(int entryId);
        List<JournalEntry> GetByUserId(string id);
    }
}
