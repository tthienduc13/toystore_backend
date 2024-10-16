using Repositories.ViewModel;
using Repositories.ViewModel.ProductViewModels;
using Services.ViewModels;

namespace Services.Interfaces;

public interface IProductService
{
    Task<ResponseModel> CreateProduct(CreateProductModel product);
    Task<ResponseModel> UpdateProduct(Guid productId, UpdateProductModel product);
    Task<ResponseModel> DeleteProduct(Guid productId);
    Task<ProductResponseModel> GetProductById(Guid productId);
    Task<List<ProductResponseModel>> GetProductAll();
}