using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Model;

namespace UserApi.Data.EFCore
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext _userDBContext;

        public UserRepository(UserDBContext userDBContext)
        {
            _userDBContext = userDBContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userDBContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(int userID)
        {
            return await _userDBContext.Users.FirstOrDefaultAsync(x => x.Id == userID);
        }

        public async Task<User> AddUser(User user)
        {
            var result = await _userDBContext.Users.AddAsync(user);
            await _userDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await _userDBContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (result == null)
                return null;
            result.Name = user.Name;
            result.Name = user.Name;
            result.Passowrd = user.Passowrd;
            result.Address = user.Address;
            result.State = user.State;
            result.Country = user.Country;
            result.EmailAddress = user.EmailAddress;
            result.ContactNumber = user.ContactNumber;
            result.EmailAddress = user.EmailAddress;
            result.MobileNumber = user.MobileNumber;
            await _userDBContext.SaveChangesAsync();
            return result;
        }

        public void DeleteUser(int userID)
        {
            throw new NotImplementedException();
        }

    }
}
