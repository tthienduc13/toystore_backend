using System.Net;
using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.ViewModel.ProductViewModels;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimService = claimService;
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
}