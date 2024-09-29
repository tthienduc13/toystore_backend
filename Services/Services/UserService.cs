

using System.Net;
using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.ViewModel.UserViewModels.Response;
using Service.Constants;
using Service.Security;
using Services.Interfaces;
using Services.ViewModels;
using ToyStore.Request.UserViewModel;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenGenerator _tokenGenerator;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ITokenGenerator tokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenGenerator = tokenGenerator;
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
            Fullname = newAccount.Fullname,
            Role = RoleConstants.CUSTOMER
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
        
        if (user is null)
        {
            return new ResponseModel(HttpStatusCode.NotFound, "User not found!");
        }

        if (!BCrypt.Net.BCrypt.Verify(requestModel.Password, user.Password))
        {
            return new ResponseModel(HttpStatusCode.BadRequest, "Wrong password!");
        }
        if (user.Isdelete is true )
        {
            return new ResponseModel(HttpStatusCode.Forbidden, "Your account is suspensed!");
        }
      
        var token = await _tokenGenerator.GenerateToken(requestModel);
        var loginResponse = _mapper.Map<LoginResponseModel>(user);
        loginResponse.Token = token;
        return new ResponseModel(HttpStatusCode.OK, "Login successfully!", loginResponse);
    }
}