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
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;

        public NotesService(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public void Create(Note note)
        {
            _notesRepository.Create(note);
        }

        public StatusModel Delete(int noteId)
        {
            var response = new StatusModel();
            var note = _notesRepository.GetById(noteId);
            if (note == null)
            {
                response.IsSuccessful = false;
                response.Message = $"There is no note with Id {noteId}";
            }

            _notesRepository.Delete(note);
            response.Message = $"There note with Id {noteId} has been successfully deleted";
            return response;
        }

        public bool FindIfNoteExistsForUser(string userId, int noteId)
        {
            var exists = false;
            var allNotes = _notesRepository.GetAll();
            if (allNotes != null)
            {
                exists = allNotes.Any(x => x.UserId == userId && x.Id == noteId);
            }
            return exists;
        }

        public List<Note> GetAllByUserId(string userId)
        {
            var allNotes = _notesRepository.GetAll();
            var userNotes = allNotes.Where(x => x.UserId == userId).ToList();
            return userNotes;
        }
    }
}
