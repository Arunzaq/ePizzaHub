using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Contracts
{
    public interface IGenericRepository<T> where T : class
    {

        void Add(T entity);
        int Commitchanges();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task <T> GetSingleItem(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
