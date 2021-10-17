﻿using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IJournalEntriesService
    {
        JournalEntry GetById(int entryId);
        List<JournalEntry> GetByUserId(string id);
        void Create(JournalEntry journalEntry);
    }
}
