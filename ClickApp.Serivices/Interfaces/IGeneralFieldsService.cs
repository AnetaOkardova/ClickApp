using ClickApp.Models;
using ClickApp.Services.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Services.Interfaces
{
    public interface IGeneralFieldsService
    {
        List<GeneralField> GetAll();
        StatusModel Create(GeneralField generalField);
        StatusModel Delete(int id);
        StatusModel Update(GeneralField generalField);
        GeneralField GetById(int id);
        List<GeneralField> GetAllWithFilter(string name, GeneralFieldCode codeSearch);
    }
}
