using epizza.Domain.Models;
using ePizza.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
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

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetSingleItem(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
