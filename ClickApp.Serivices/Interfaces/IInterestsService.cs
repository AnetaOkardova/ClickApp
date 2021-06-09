using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IInterestsService
    {
        List<Interest> GetAll();
        StatusModel Create(Interest interest);
        StatusModel Delete(int id);
        StatusModel Update(Interest interest);
        Interest GetById(int id);
    }
}
