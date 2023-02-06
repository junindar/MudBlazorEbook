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
    public class UserService : IUserService
    {
       
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetAllAsNoTrackingAsync()
        {
            return await _userRepository.TableNoTracking().ToListAsync();
        
        }

      

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> InsertAsync(User obj)
        {
            await _userRepository.AddAsync(obj);
          
            return obj;
        }

        public async Task<User> UpdateAsync(User obj)
        {
            await _userRepository.UpdateAsync(obj);
 
            return obj;
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await GetByIdAsync(id);
            await _userRepository.RemoveAsync(obj);
        }

        public async Task ClearTrackerAsync()
        {
           await  Task.Run(() => _userRepository.ClearTracker());
        }

        public async Task<User> CheckLoginAsync(User obj)
        {
            var user = await _userRepository.Table().Where(c => c.Username.ToLower()
                                                         == obj.Username.ToLower() &&
                                                         c.Password == obj.Password).FirstOrDefaultAsync();

            return user;

        }
    }
}
