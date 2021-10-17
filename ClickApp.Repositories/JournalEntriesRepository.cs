using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories
{
    public class JournalEntriesRepository : BaseRepository<JournalEntry>, IJournalEntriesRepository
    {
        private ApplicationDbContext _context { get; set; }
        public JournalEntriesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
