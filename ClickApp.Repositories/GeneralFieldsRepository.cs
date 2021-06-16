using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class GeneralFieldsRepository : IGeneralFieldsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public GeneralFieldsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<GeneralField> GetAll()
        {
           return _context.GeneralFields.ToList();
        }

        public bool CheckIfExists(string name, GeneralFieldCode code)
        {
            return _context.GeneralFields.Any(x=>x.Name.ToLower() == name.ToLower() && x.Code == code);
        }

        public void Create(GeneralField generalField)
        {
            _context.GeneralFields.Add(generalField);
            _context.SaveChanges();
        }

        public GeneralField GetById(int id)
        {
            return _context.GeneralFields.FirstOrDefault(x=>x.Id == id);
        }

        public void Delete(GeneralField generalField)
        {
            _context.GeneralFields.Remove(generalField);
            _context.SaveChanges();
        }

        public void Update(GeneralField generalField)
        {
            _context.GeneralFields.Update(generalField);
            _context.SaveChanges();
        }

        public List<GeneralField> GetAllWithFilter(string name, GeneralFieldCode codeSearch)
        {
            var query = _context.GeneralFields.AsQueryable();
            if (name != null)
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            if (codeSearch != GeneralFieldCode.NONE)
            {
                query = query.Where(x => x.Code == codeSearch);
            }

            var generalFields = query.ToList();
            return generalFields;

        }
    }
}
