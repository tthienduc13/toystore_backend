using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class BaseRepository<T>: IRepository<T> where T : class
{
    private readonly MyDbContext _context;
    protected readonly DbSet<T> _entities;

    public BaseRepository(MyDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }
    

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }
}