using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MyDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetCategoryById(Guid categoryId)
    {
        return await _entities.Where(x => x.Isdelete == false).FirstOrDefaultAsync(x => x.Id.Equals(categoryId));
    }

    public async Task<bool> CheckExistedCategory(Guid categoryId)
    {
        return await _entities.AnyAsync(x => x.Id.Equals(categoryId) && x.Isdelete == false);
    }
}