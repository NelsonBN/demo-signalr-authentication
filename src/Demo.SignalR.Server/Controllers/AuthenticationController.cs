using Demo.SignalR.Server.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.SignalR.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationConfig _config;

        public AuthenticationController(
            IOptions<AuthenticationConfig> config
        )
        {
            this._config = config.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = Guid.NewGuid();
            var email = "fake@email.fake";
           
            var generatedAt = DateTime.UtcNow;
            var expires = generatedAt.AddHours(this._config.JWT_VALIDATION_HOURS);

            var generatedAtTimestamp = Convert.ToUInt64(generatedAt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds)
                .ToString().Substring(0, 10);

            var claims = new[]
            {
                new Claim("at_hash", Guid.NewGuid().ToString().Replace("-", string.Empty)),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim("email_verified", true.ToString().ToLower()),
                new Claim(JwtRegisteredClaimNames.Nonce, Guid.NewGuid().ToString().Replace("-", string.Empty) + "-" + Guid.NewGuid().ToString().Replace("-", string.Empty)),
                new Claim("token_use", "id"),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Iat, generatedAtTimestamp),
                new Claim("auth_time", generatedAtTimestamp),
            };

            // Generate a key based in sync algorithm
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config.JWT_SALT));

            // Generate digital signature based in private key abd Hmac algorithm
            var credenciais = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // JWT payload
            var payloadToken = new JwtSecurityToken(
                issuer: this._config.JWT_ISSUER,
                audience: this._config.JWT_AUDIENCE,
                claims: claims,
                expires: expires,
                signingCredentials: credenciais
            );

            var token = new JwtSecurityTokenHandler().WriteToken(payloadToken);

            return this.Ok(token);
        }
    }
}