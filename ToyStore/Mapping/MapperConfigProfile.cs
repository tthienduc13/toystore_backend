using AutoMapper;
using Repositories.Entities;
using ToyStore.Request.UserViewModel;

namespace ToyStore.Mapping;

public class MapperConfigProfile : Profile
{
    public MapperConfigProfile()
    {
        CreateMap<RegisterModelRequest, User>().ReverseMap();
    }
}