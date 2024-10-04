using Microsoft.EntityFrameworkCore.Storage;
using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _context;
    private IDbContextTransaction _transaction;

    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);
    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);

    public UnitOfWork(MyDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null)
        {
            return;
        }

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction is null)
        {
            throw new InvalidOperationException("A transaction has not been started.");
        }

        try
        {
            await _transaction.CommitAsync();
            _transaction.Dispose();
            _transaction = null;
        }
        catch (Exception)
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
            }

            throw;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
            }
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}