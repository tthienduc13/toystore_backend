using Repositories.Entities;

namespace Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> CheckUsernameEmailExist(string username, string email);

    Task<User?> GetUserByUsername(string username);
}