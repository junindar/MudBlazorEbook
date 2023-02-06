using DataService.Data;
using DataService.Entities;
using DataService.IService;
using DataService.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataService.Service
{
    public class CategoryService : ICategoryService
    {
      
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
      
        public async Task<List<Category>> GetAllAsNoTrackingAsync()
        {
            var result = await _categoryRepository.TableNoTracking().ToListAsync();
            await ClearTrackerAsync();
            return result;
        }
        public async Task ClearTrackerAsync()
        {
            await Task.Run(() => _categoryRepository.ClearTracker());
        }

        public async Task<List<Category>> GetAllAsync()
        {
           var result= await _categoryRepository.Table().ToListAsync();
           await ClearTrackerAsync();
           return result;
        }


        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> InsertAsync(Category obj)
        {

            await _categoryRepository.AddAsync(obj);
            return obj;
        }

        public async Task<Category> UpdateAsync(Category obj)
        {


            await _categoryRepository.UpdateAsync(obj);
            
            return obj;
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await  GetByIdAsync(id);
            await _categoryRepository.RemoveAsync(obj);
        }
    }

}
