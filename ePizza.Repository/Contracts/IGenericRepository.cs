using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repository.Contracts
{
    public interface IGenericRepository<T> where T : class
    {

        void Add(T entity);
        int Commitchanges();
        IEnumerable<T> GetAll();
    }
}
