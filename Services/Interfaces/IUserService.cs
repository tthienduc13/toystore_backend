using Repositories.Entities;
using Services.ViewModels;
using ToyStore.Request.UserViewModel;

namespace Services.Interfaces;

public interface IUserService
{
    Task<ResponseModel> Register(User newAccount);
    
    Task<ResponseModel> LoginByCredentials(LoginRequestModel requestModel);
}