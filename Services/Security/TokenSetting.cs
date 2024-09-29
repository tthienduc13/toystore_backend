using Microsoft.Extensions.Configuration;

namespace Service.Security;

public class TokenSetting
{
    public string? SecurityKey { get; set; }
    public double? TokenExpiry { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }

    public TokenSetting()
    {
        GetSettingConfig();
    }

    private void GetSettingConfig()
    {
        IConfiguration config = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

            .AddJsonFile("appsettings.json", true, true)

            .Build();

        this.SecurityKey = config["JWTSetting:SecurityKey"];
        this.Issuer = config["JWTSetting:ValidIssuer"];
        this.Audience = config["JWTSetting:ValidAudience"];
        this.TokenExpiry = Convert.ToDouble(config["JWTSetting:TokenExpiry"]);

    }
}