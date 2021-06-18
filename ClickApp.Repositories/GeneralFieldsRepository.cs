using ClickApp.Data;
using ClickApp.Models;
using ClickApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class GeneralFieldsRepository : BaseRepository<GeneralField>, IGeneralFieldsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public GeneralFieldsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<GeneralField> GetAllWithFilter(string name, GeneralFieldCode codeSearch)
        {
            var generalFields = GetAll();

            if (name != null)
            {
                generalFields = generalFields.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            if (codeSearch != GeneralFieldCode.NONE)
            {
                generalFields = generalFields.Where(x => x.Code == codeSearch).ToList();
            }

            return generalFields;
        }
    }
}
