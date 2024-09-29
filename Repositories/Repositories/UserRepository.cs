using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class UserRepository: BaseRepository<User>, IUserRepository
{
    public UserRepository(MyDbContext context) : base(context)
    {
        
    }

    public async Task<bool> CheckUsernameEmailExist(string username, string email)
    {
        return await _entities.AnyAsync(x => x.Email.Equals(email) || x.Username.Equals(username) 
                                                                   || x.Isdelete == false );
    }
    
    public async Task<User> GetUserByUsername(string username)
    {
        return await _entities.Where(
            x => x.Username == username || x.Email == username
        ).FirstOrDefaultAsync();
    }
}