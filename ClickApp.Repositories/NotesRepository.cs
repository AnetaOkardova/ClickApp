using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories
{
    public class NotesRepository : BaseRepository<Note>, INotesRepository
    {
        private ApplicationDbContext _context { get; set; }
        public NotesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
