

using System.Net;
using Repositories.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModel> Register(User newAccount)
    {
        var isExisted =await _unitOfWork.UserRepository.CheckUsernameEmailExist(newAccount.Email, newAccount.Username);
        if (isExisted)
        {
            return new ResponseModel(HttpStatusCode.Conflict, "Username or email existed!", null);
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newAccount.Password);

        var newUser = new User()
        {
            Username = newAccount.Username,
            Password = hashedPassword,
            Email = newAccount.Email,
            Fullname = newAccount.Fullname
        };

        await _unitOfWork.UserRepository.AddAsync(newUser);

        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0
            ? new ResponseModel(HttpStatusCode.OK, "Account created", null)
            : new ResponseModel(HttpStatusCode.BadRequest, "Create account failed", null);
    }
}