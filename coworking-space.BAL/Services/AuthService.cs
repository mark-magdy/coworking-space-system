using CO_Working_Space;
using coworking_space.BAL.Dtos.UserDTO;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Services {
    public class AuthService : IAuthService {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration config, IUserRepository userRepository) {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<string?> LoginAsync(string email, string password) {
            var user = await _userRepository.GetByEmailAsync(email);
            //return user.Name;
            if (user == null || !VerifyPassword(password, user.Password))
                return null;

            return GenerateJwtToken(user);
        }

        public async Task<string?> RegisterAsync(UserCreateDto dto) {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                return null;

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                UniversityName = dto.UniversityName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = "User"
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user) {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("name", user.Name)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password) {
            // Use proper password hashing like BCrypt or PBKDF2 in production
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string input, string hashed) {
            return HashPassword(input) == hashed;
        }
    }
}