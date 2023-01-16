using DataService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entities;
using DataService.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataService.Service
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<List<Book>> GetAllAsNoTrackingAsync()
        {
            var result= await _bookRepository.TableNoTracking().Include(c=>c.Category).ToListAsync();
            return result;

        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.Table().ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> InsertAsync(Book obj)
        {
            await _bookRepository.AddAsync(obj);
          
            return obj;
        }

        public async Task<Book> UpdateAsync(Book obj)
        {
            await _bookRepository.UpdateAsync(obj);
 
            return obj;
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await GetByIdAsync(id);
            await _bookRepository.RemoveAsync(obj);
        }

        public async Task ClearTrackerAsync()
        {
           await  Task.Run(() => _bookRepository.ClearTracker());
        }

        public async Task<List<Book>> GetRandomBooksAsync(string cat)
        {
            return await _bookRepository.Table().Where(c => c.Category.Nama == cat)
                .OrderBy(r => Guid.NewGuid()).Include(b => b.Category).ToListAsync();
        }

        public async Task<List<Book>> GetRelatedRandomBooksAsync(string cat, int bookId)
        {
            return await _bookRepository.Table().Where(c => c.Category.Nama == cat && c.ID!=bookId)
                .OrderBy(r => Guid.NewGuid()).Include(b => b.Category).ToListAsync();
        }

        public async Task<List<Book>> SearchBookAsync(string keyword)
        {
            var result = await _bookRepository.TableNoTracking().Where(c=>c.Judul.ToLower().Contains(keyword) ||
                                                                          c.Penerbit.ToLower().Contains(keyword)).Include(c => c.Category).ToListAsync();
            await ClearTrackerAsync();
            return result;
        }
    }
}
