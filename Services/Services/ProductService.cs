using System.Net;
using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.ViewModel;
using Repositories.ViewModel.ProductViewModels;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;
    private readonly ICloudinaryService _cloudinaryService;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICloudinaryService cloudinaryService) 
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimService = claimService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ResponseModel> CreateProduct(CreateProductModel product)
    {
        var checkCategory = await _unitOfWork.CategoryRepository.CheckExistedCategory((Guid)product.Categoryid);
        if (!checkCategory)
        {
            return new ResponseModel(HttpStatusCode.BadRequest, "Category is not existed!");
        }
        
        var newProduct = new Product();
        newProduct = _mapper.Map<Product>(product);
        newProduct.Createdat = DateTime.Now;
        newProduct.Isdeleted = false;
        newProduct.Createdby = _claimService.GetCurrentUserId;
        var uploadResult = await _cloudinaryService.UploadAsync(product.Img);
        if (uploadResult.Error != null)
        {
            return new ResponseModel(HttpStatusCode.BadRequest, uploadResult.Error.Message);
        }
        newProduct.Img = uploadResult.Url.ToString();
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.ProductRepository.AddAsync(newProduct);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                await _unitOfWork.CommitTransactionAsync();
                return new ResponseModel(HttpStatusCode.OK, "Create product successfully!");
            }
            
            await _unitOfWork.RollbackTransactionAsync();
            return new ResponseModel(HttpStatusCode.BadRequest, "Create product failed!");
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new ResponseModel(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<ResponseModel> UpdateProduct(Guid productId, UpdateProductModel product)
    {
        var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
        if (existingProduct == null)
        {
            return new ResponseModel(HttpStatusCode.NotFound, "Product not found!");
        }
        string imageUrl = null;
        if (product.Img != null)
        {
            var uploadResult = await _cloudinaryService.UploadAsync(product.Img);
            if (uploadResult.Error != null)
            {
                return new ResponseModel(HttpStatusCode.BadRequest, uploadResult.Error.Message);
            }

            imageUrl = uploadResult.Url.ToString();
        }
        var updateProduct = new Product()
        {
            Id = existingProduct.Id,
            Name = product.Name ?? existingProduct.Name,
            Description = product.Description ?? existingProduct.Description,
            Img = imageUrl ?? existingProduct.Img,
            Price = product.Price ?? existingProduct.Price,
            Brand = product.Brand ?? existingProduct.Brand,
            Categoryid = product.Categoryid ?? existingProduct.Categoryid
        };
        var result = await _unitOfWork.ProductRepository.UpdateProduct(updateProduct);
        if(result is false){
            return new ResponseModel(HttpStatusCode.BadRequest,"update fail");
        }
        return new ResponseModel(HttpStatusCode.OK,"update success");
    }

    public async Task<ResponseModel> DeleteProduct(Guid productId)
    {
        var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
        if (existingProduct == null)
        {
            return new ResponseModel(HttpStatusCode.NotFound, "Product not found!");
        }
        var result = await _unitOfWork.ProductRepository.DeleteProductById(existingProduct.Id);
        if(result is false){
            return new ResponseModel(HttpStatusCode.BadRequest,"delete fail");
        }
        return new ResponseModel(HttpStatusCode.OK,"delete success");
    }

    public async Task<ProductResponseModel> GetProductById(Guid productId)
    {
        var product = await _unitOfWork.ProductRepository.GetProductById(productId);
        var productViewModel = _mapper.Map<ProductResponseModel>(product);
        return productViewModel;
    }
    public async Task<List<ProductResponseModel>> GetProductAll()
    {
        var product = await _unitOfWork.ProductRepository.GetAllProducts();
        var productViewModel = _mapper.Map<List<ProductResponseModel>>(product);
        return productViewModel;
    }
}
