using Repositories.Entities;
using Services.ViewModels;

namespace Services.Interfaces;

public interface IUserService
{
    Task<ResponseModel> Register(User newAccount);
}