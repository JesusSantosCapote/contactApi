using DataAccess.DataContexts;
using DataAccess.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagerContext _userManagerContext;
        public UserRepository(UserManagerContext userManagerContext) 
        {
            _userManagerContext = userManagerContext;
        }
        public async Task<UserEntity> GetUserByNameAsync(string username)
        {
            UserEntity user = await _userManagerContext.Users.Where(u => u.UserName == username).FirstOrDefaultAsync();

            if (user is default(UserEntity)) return null;
            return user;
        }
    }
}
