using Repositories.Entities;
using Repositories.ViewModel.CategoryViewModels;
using Services.ViewModels;

namespace Services.Interfaces;

public interface ICategoryService
{
    Task<ResponseModel> CreateCategory(CreateCategoryModel categoryModel);
}