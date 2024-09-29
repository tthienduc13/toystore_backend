

using System.Net;
using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.ViewModel.UserViewModels.Respond;
using Services.Interfaces;
using Services.ViewModels;
using ToyStore.Request.UserViewModel;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
    
    public async Task<ResponseModel> LoginByCredentials(LoginRequestModel requestModel)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsername(requestModel.Username);
        // var emailUser = await _userRepository.GetUserByEmail(login.Username);
        
        if (user == null)
        {
            return new ResponseModel(HttpStatusCode.NotFound, "UserName or Email not existed or you have register with Google");
        }

        if (!BCrypt.Net.BCrypt.Verify(requestModel.Password, user.Password))
        {
            return new ResponseModel(HttpStatusCode.BadRequest, "Wrong password!");
        }
        // need add verify email to continute
        if (user.Isdelete == true)
        {
            return new ResponseModel(HttpStatusCode.Forbidden, "Your account is suspense!");
        }
        //
        // LoginDtoRequest loginDtoRequest = new LoginDtoRequest()
        // {
        //     Username = login.Username,
        //     Password = login.Password
        // };
        //
        // var token = _tokenGenerator.GenerateToken(loginDtoRequest);
        var loginResponse = _mapper.Map<LoginResponseModel>(user);
        // loginResponse.Token = token;
        return new ResponseModel(HttpStatusCode.OK, "Login successfully!", loginResponse);
    }
}