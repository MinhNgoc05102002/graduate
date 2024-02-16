using GP.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GP.Common.Helpers
{
    public class AuthHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public static void CreatePassHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computerHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Name, account.Username),
                //new Claim(ClaimTypes.Email, account.Email),
                //new Claim(ClaimTypes.Role, account.Role)
                new Claim("username", account.Username),
                new Claim("email", account.Email),
                new Claim("role", account.Role),
                new Claim("avatar", account.Avatar),
                new Claim("role", account.Role),
                new Claim("status", account.Status),
                new Claim("createdAt", account.Status),
                new Claim("createdAt", account.Status),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string GetCurrentUsername()
        {
            var username = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                //username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                username = _httpContextAccessor.HttpContext.User.FindFirstValue("username");
            }
            return username;
        }
    }
}
