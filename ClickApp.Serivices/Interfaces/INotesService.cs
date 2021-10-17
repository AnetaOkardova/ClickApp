using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface INotesService
    {
        List<Note> GetAllByUserId(string userId);
        void Create(Note note);
        bool FindIfNoteExistsForUser(string userId, int noteId);
        StatusModel Delete(int noteId);
    }
}
