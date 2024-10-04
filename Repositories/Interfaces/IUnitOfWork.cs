namespace Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    
    IProductRepository ProductRepository { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}