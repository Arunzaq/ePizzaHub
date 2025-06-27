using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class GenericRepository<T> : Contracts.IGenericRepository<T> where T : class
    {
        protected epizzaHubDBContext _dbContext;

        public GenericRepository(epizzaHubDBContext dbContext)
        {
            _dbContext = dbContext;
        }



        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public int Commitchanges()
        {
           return _dbContext.SaveChanges();
            
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }
    }
}
