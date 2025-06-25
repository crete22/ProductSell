using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Product_sell
{
    public class DatabaseFacade
    {
      
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

       
        public void UpdateEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

       
        public void DeleteEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

    
        public T GetEntityById<T>(int id) where T : class
        {
            // Метод Find специально оптимизирован для поиска по первичному ключу.
            return _context.Set<T>().Find(id);
        }

        public List<T> GetAllEntities<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }
    }
}
