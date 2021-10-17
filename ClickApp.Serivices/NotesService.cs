using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using ClickApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;

        public NotesService(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public List<Note> GetAllByUserId(string userId)
        {
            var allNotes = _notesRepository.GetAll();
            var userNotes = allNotes.Where(x => x.UserId == userId).ToList();
            return userNotes;
        }
    }
}
