﻿using ClickApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickApp.Repositories.Interfaces
{
    public interface IGeneralFieldsRepository
    {
        List<GeneralField> GetAll();
        bool CheckIfExists(string name);
        void Create(GeneralField generalField);
        GeneralField GetById(int id);
        void Delete(GeneralField generalField);
        void Update(GeneralField generalField);
    }
}