namespace Repositories.ViewModel.UserViewModels.Response;

public class LoginResponseModel
{
    public Guid Id { get; set; }

    public string? Fullname { get; set; }
    
    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Img { get; set; }
    
    public int? Role { get; set; }
    
    public string? Token { get; set; }
}