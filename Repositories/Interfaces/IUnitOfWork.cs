namespace Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICategoryRepository CategoryRepository { get; }

    Task<int> SaveChangesAsync();
}