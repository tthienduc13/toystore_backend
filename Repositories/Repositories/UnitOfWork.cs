using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _context;

    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;

    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);


    public UnitOfWork(MyDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}