using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface ISkillsRepository
    {
        List<Skill> GetAll();
        bool CheckIfExists(string name);
        void Create(Skill skill);
        Skill GetById(int id);
        void Delete(Skill skill);
        void Update(Skill skill);
    }
}

