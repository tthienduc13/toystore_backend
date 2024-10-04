using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.ViewModel.ProductViewModels;
using Services.Interfaces;
using Services.ViewModels;
using ToyStore.Common;

namespace ToyStore.Controller;

[Route("api/product")]
[ApiController]

public class ProductController: ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    
    //Validator
    private readonly ValidationHelper<CreateProductModel> _productValidation;

    public ProductController(IProductService productService, IMapper mapper, ValidationHelper<CreateProductModel> productValidation)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _productValidation = productValidation;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ResponseModel> CreateProduct([FromBody, Required] CreateProductModel model)
    {
        var (isValid, response) = await _productValidation.ValidateAsync(model);
        if (!isValid)
        {
            return response;
        } 
        return await _productService.CreateProduct(model);
    }
}