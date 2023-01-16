using DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.IService
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsNoTrackingAsync();
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> InsertAsync(Book obj);
        Task<Book> UpdateAsync(Book obj);
        Task DeleteAsync(int id);
        Task ClearTrackerAsync();
        Task<List<Book>> GetRandomBooksAsync(string cat);
        Task<List<Book>> GetRelatedRandomBooksAsync(string cat, int bookId);

        Task<List<Book>> SearchBookAsync(string keyword);
    }
}
