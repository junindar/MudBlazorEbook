using DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.IService
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsNoTrackingAsync();
      
        Task<User> GetByIdAsync(int id);
        Task<User> InsertAsync(User obj);
        Task<User> UpdateAsync(User obj);
       // Task<User> ChangePasswordAsync(User obj);
        Task DeleteAsync(int id);
        Task ClearTrackerAsync();
        Task<User> CheckLoginAsync(User obj);
    }
}
