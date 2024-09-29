using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using Service.Constants;
using ToyStore.Request.UserViewModel;

namespace Service.Security;

public class TokenGenerator : ITokenGenerator
{
    private readonly TokenSetting _tokenSetting;
    private readonly IUserRepository _userRepository;

    public TokenGenerator(IUserRepository userRepository)
    {
        _tokenSetting = new TokenSetting();
        _userRepository = userRepository;
    }

    public async Task<string> GenerateToken(LoginRequestModel login)
    {
        var user = await _userRepository.GetUserByUsername(login.Username);
        if (user == null)
        {
            return "Error! Unauthorized.";
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_tokenSetting.SecurityKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Role",user.Role == RoleConstants.OWNER ? "Owner" : "Customer" ),
                new Claim("Fullname", user.Fullname)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _tokenSetting.Issuer,
            Audience = _tokenSetting.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}