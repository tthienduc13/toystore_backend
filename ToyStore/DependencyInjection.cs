using FluentValidation;
using ToyStore.Mapping;
using Repositories.Interfaces;
using Repositories.Repositories;
using Repositories.ViewModel.ProductViewModels;
using Service.Security;
using Services.Interfaces;
using Services.Services;
using ToyStore.Common;
using ToyStore.Validator.Product;

namespace ToyStore;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiService(this IServiceCollection services)
    {
        //Repository
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        //Service
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IClaimService, ClaimsService>();
        services.AddScoped<IProductService, ProductService>();
        
        //Others
        services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddHttpContextAccessor();
        
        //Validation
        services.AddScoped(typeof(ValidationHelper<>));
        services.AddScoped<IValidator<CreateProductModel>, CreateProductValidator>();

        return services;
    } 
}