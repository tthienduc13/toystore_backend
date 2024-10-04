using System.Security.Claims;

public class AuthenTools
{
    public static string GetCurrentAccountId(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        }
        return null;
    }
    
  
}