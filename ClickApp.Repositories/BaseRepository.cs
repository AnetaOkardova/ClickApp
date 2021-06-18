using ClickApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickApp.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private ApplicationDbContext _context { get; set; }
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public virtual void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }


        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public virtual T GetById(int entityId)
        {
            return _context.Set<T>().Find(entityId);
        }
    }
}
