using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface IInterestsRepository
    {
        List<Interest> GetAll();
        bool CheckIfExists(string name);
        void Create(Interest interest);
        Interest GetById(int id);
        void Delete(Interest interest);
        void Update(Interest interest);
    }

}
