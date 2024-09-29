using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services.Interfaces;
using Services.ViewModels;
using ToyStore.Request.UserViewModel;

namespace ToyStore.Controller;

[Route("api/auth")]
[ApiController]

public class AuthController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("register")]

    public async Task<ResponseModel> Register([FromBody, Required] RegisterModelRequest model)
    {
        var user = _mapper.Map<User>(model);
        return await _userService.Register(user);
    }
}