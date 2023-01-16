using DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.IService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsNoTrackingAsync();
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> InsertAsync(Category obj);
        Task<Category> UpdateAsync(Category obj);
        Task DeleteAsync(int id);
    }
}
