using Repositories.Entities;

namespace Repositories.Interfaces;

public interface ICategoryRepository: IRepository<Category>
{
    Task<Category?> GetCategoryById(Guid categoryId);

    Task<bool> CheckExistedCategory(Guid categoryId);
}