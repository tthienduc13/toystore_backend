using Repositories.Context;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MyDbContext context) : base(context)
    {
    }

  
}