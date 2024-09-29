using AutoMapper;
using Repositories.Entities;
using Repositories.ViewModel.UserViewModels.Respond;
using ToyStore.Request.UserViewModel;

namespace ToyStore.Mapping;

public class MapperConfigProfile : Profile
{
    public MapperConfigProfile()
    {
        CreateMap<RegisterRequestModel, User>().ReverseMap();
        CreateMap<LoginRequestModel, User>().ReverseMap();
        CreateMap<LoginResponseModel, User>().ReverseMap();
    }
}