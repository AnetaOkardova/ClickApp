using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories
{
    public class JournalThemesRepository : BaseRepository<JournalTheme>, IJournalThemesRepository
    {
        private ApplicationDbContext _context { get; set; }
        public JournalThemesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
