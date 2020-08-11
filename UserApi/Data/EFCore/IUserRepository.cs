using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Model;

namespace UserApi.Data.EFCore
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int userID);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        void DeleteUser(int userID);

    }
}
