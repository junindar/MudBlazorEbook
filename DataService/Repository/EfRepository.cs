using DataService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Repository
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly PustakaDbContext _dbContext;
        public EfRepository(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
           // var tes= await _dbContext.Set<T>().FindAsync(id);
          
            return await _dbContext.Set<T>().FindAsync(id); 
        }

        public async Task<T> AddAsync(T newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException(nameof(newItem));
            _dbContext.Set<T>().Add(newItem);
            _dbContext.Entry(newItem).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();

            return newItem;
        }

        public async Task<T> UpdateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task RemoveAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _dbContext.Entry(item).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public  IQueryable<T> Table()
        {
            return _dbContext.Set<T>();
        }
        public IQueryable<T> TableNoTracking()
        {
            return   _dbContext.Set<T>().AsNoTracking();
        }

        public void Detach(T item)
        {
            _dbContext.Entry(item).State = EntityState.Detached;
        }

        public void ClearTracker()
        {
            _dbContext.ChangeTracker.Clear();
        }
    }
}
