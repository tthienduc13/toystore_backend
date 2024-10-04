using AutoMapper;
using Repositories.Entities;
using Repositories.ViewModel.ProductViewModels;
using Repositories.ViewModel.UserViewModels.Response;
using ToyStore.Request.UserViewModel;

namespace ToyStore.Mapping;

public class MapperConfigProfile : Profile
{
    public MapperConfigProfile()
    {
        CreateMap<RegisterRequestModel, User>().ReverseMap();
        CreateMap<LoginRequestModel, User>().ReverseMap();
        CreateMap<User, LoginResponseModel>();
        
        //Product
        CreateMap<CreateProductModel, Product>();
    }
}