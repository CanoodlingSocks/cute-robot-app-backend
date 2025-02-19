﻿using Microsoft.IdentityModel.Tokens;
using Service.DTO;
using Service.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_nebula_backend.JWTAuthentication
{
    public class TokenService : ITokenService
    {
        public IConfiguration _configuration;
        private readonly IUserService _userService;

        public TokenService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public string CreateJWTToken(SuccessfulLoginDTO result)
        {

            var userRole = _userService.GetUserRole(result);

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", result.Id.ToString()),
                        new Claim("Username", result.Username.ToString()),
                        new Claim("Roles", userRole)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10080),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
