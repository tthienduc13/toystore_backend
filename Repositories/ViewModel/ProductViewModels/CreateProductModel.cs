﻿
using Microsoft.AspNetCore.Http;

namespace Repositories.ViewModel.ProductViewModels;


public class CreateProductModel
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public IFormFile? Img { get; set; }

    public double? Price { get; set; }

    public string? Brand { get; set; }

    public Guid? Categoryid { get; set; }
}