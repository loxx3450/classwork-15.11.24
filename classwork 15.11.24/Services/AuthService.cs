using classwork.Core.Interfaces;
using classwork.Core.Models;
using classwork.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace classwork_15._11._24.Services
{
    public class AuthService : IAuthService
    {
        private const string EmailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        private const string PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

        private readonly DataContext _context;
        private IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task Register(string email, string password)
        {
            User user = new User 
            { 
                Email = email, 
                Password = password 
            };

            ValidateUser(user);

            if (! _context.Users.Any(u => u.Email == email))
            {
                await _context.Users.AddAsync(user);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<(User?, string)> Login(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));

            return (user, GenerateToken(user));
        }

        private void ValidateUser(User user)
        {
            if (!Regex.IsMatch(user.Email, EmailPattern))
            {
                throw new ArgumentNullException("Email is invalid", nameof(user.Email));
            }

            if (!Regex.IsMatch(user.Password, PasswordPattern))
            {
                throw new ArgumentNullException("Password is invalid", nameof(user.Password));
            }
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, nameof(User)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration.GetSection("AppSettings:ExpireTime").Value!)),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
