using Repositories.ViewModel.ProductViewModels;
using Services.ViewModels;

namespace Services.Interfaces;

public interface IProductService
{
    Task<ResponseModel> CreateProduct(CreateProductModel product);
}