using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;

public class ClaimsService : IClaimService
{
    public ClaimsService(IHttpContextAccessor httpContextAccessor)
    {
      
        var identity = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
        var extractedId = AuthenTools.GetCurrentAccountId(identity);
       
        GetCurrentUserId = string.IsNullOrEmpty(extractedId) ? Guid.Empty : new Guid(extractedId);
       

    }
    public Guid GetCurrentUserId { get; }
    
}