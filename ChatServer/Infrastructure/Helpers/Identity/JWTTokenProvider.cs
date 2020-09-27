namespace ChatServer.Infrastructure.Helpers.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AppServices.UserService.Interfaces;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class JWTTokenProvider : IJWTTokenProvider
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public JWTTokenProvider(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<string> GetJWTToken(string UserName)
        {
            var loggedUser = await _userService.GetByUserName(UserName);

            if (loggedUser != null)
            {
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("UserID", loggedUser.Id),
                        new Claim(_options.ClaimsIdentity.UserNameClaimType, loggedUser.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"])), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }

            return String.Empty;
        }
    }
}
