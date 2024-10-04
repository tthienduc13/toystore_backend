using ToyStore.Mapping;
using Repositories.Interfaces;
using Repositories.Repositories;
using Service.Security;
using Services.Interfaces;
using Services.Services;

namespace ToyStore;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiService(this IServiceCollection services)
    {
        //Repository
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        
        //Service
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IClaimService, ClaimsService>();
        
        //Others
        services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddHttpContextAccessor();

        return services;
    } 
}