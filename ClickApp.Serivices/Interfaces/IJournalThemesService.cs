using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IJournalThemesService
    {
        bool CheckIfExists(string userId, string theme);
        StatusModel Create(ApplicationUser user, string theme);
        List<JournalTheme> GetAllByUserId(string userId);
        List<JournalTheme> GetAllByName(string themeName);
        void Delete(string userId, string theme);
    }
}
