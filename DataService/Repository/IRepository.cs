using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Repository
{
    public interface IRepository<T> where T : class
    {

        Task <T> GetByIdAsync(int id);
        Task<T> AddAsync(T newItem);

        Task<T> UpdateAsync(T item);
       
        Task RemoveAsync(T item);
       
        IQueryable<T> Table();
        IQueryable<T> TableNoTracking();

        void Detach(T item);
        void ClearTracker();

    }
}
