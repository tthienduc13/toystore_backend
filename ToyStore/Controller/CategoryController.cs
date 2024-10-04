using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.ViewModel.CategoryViewModels;
using Services.Interfaces;
using Services.ViewModels;
using ToyStore.Request.UserViewModel;

namespace ToyStore.Controller;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<ResponseModel> CreateCategory([FromBody, Required] CreateCategoryModel requestModel)
    {
        return await _categoryService.CreateCategory(requestModel);
    }
}