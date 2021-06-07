using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface ISkillsService
    {
        List<Skill> GetAll();
        StatusModel Create(Skill skill);
        StatusModel Delete(int id);
        StatusModel Update(Skill skill);
        Skill GetById(int id);
    }
}
