using System.Net;
using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.ViewModel.CategoryViewModels;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimService = claimService;
    }

    public async Task<ResponseModel> CreateCategory(CreateCategoryModel category)
    {
        var newCategory = new Category()
        {
            Categoryname = category.CategoryName,
            Createdat = DateTime.Now,
            Isdelete = false
        };
        
        await _unitOfWork.CategoryRepository.AddAsync(newCategory);
        
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0
            ? new ResponseModel(HttpStatusCode.OK, "Category created successfully", null)
            : new ResponseModel(HttpStatusCode.BadRequest, "Create category failed", null);
    }
}