namespace Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    
    void Update(T entity);
    
    Task<T?> GetByIdAsync(Guid? id);
    
    IQueryable<T> GetAll();
}