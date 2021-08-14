using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Demo.SignalR.Server.Middlewares
{
    public class SampleTokenValidation : TokenValidationParameters
    {
        public SampleTokenValidation(IConfiguration configuration)
        {
            this.ValidateIssuer = true;
            this.ValidateAudience = true;
            this.ValidateLifetime = true;
            this.ValidAudience = configuration[nameof(AuthenticationConfig.JWT_AUDIENCE)];
            this.ValidIssuer = configuration[nameof(AuthenticationConfig.JWT_ISSUER)];
            this.ValidateIssuerSigningKey = true;

            var saltKey = configuration[nameof(AuthenticationConfig.JWT_SALT)];
            var step = Encoding.UTF8.GetBytes(saltKey);
            this.IssuerSigningKey = new SymmetricSecurityKey(step);
        }
    }
}