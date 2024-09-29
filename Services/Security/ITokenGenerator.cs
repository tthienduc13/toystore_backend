using ToyStore.Request.UserViewModel;

namespace Service.Security;

public interface ITokenGenerator
{
    Task<string> GenerateToken(LoginRequestModel login);
}