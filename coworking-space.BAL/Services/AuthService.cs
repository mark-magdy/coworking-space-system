using CO_Working_Space;
using coworking_space.BAL.Dtos.UserDTO;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly UserManager<User> _userManager;
        public AuthService(IConfiguration config, IUserRepository userRepository, UserManager<User>userManager) {
            _config = config;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<string?> LoginAsync(string email, string password) {
            var user = await _userManager.FindByEmailAsync(email);
            //return user.Name;
            if (user == null )
                return null;
            var check = await _userManager.CheckPasswordAsync(user, password);
            if (!check)
                return null;


            return GenerateJwtToken(user);
        }

        public async Task<string?> RegisterAsync(UserCreateDto dto) {
            //var existing = await _userRepository.GetByEmailAsync(dto.Email);
            //if (existing != null)
            //    return null;

            //var user = new User
            //{
            //    Name = dto.Name,
            //    Email = dto.Email,
            //    Password = HashPassword(dto.Password),
            //    PhoneNumber = dto.PhoneNumber,
            //    UniversityName = dto.UniversityName,
            //    IsActive = true,
            //    CreatedAt = DateTime.UtcNow,
            //    UpdatedAt = DateTime.UtcNow,
            //    Role = "User"
            //};
            var user = new User
            {
                UserName = dto.Email,
                Name = dto.Name,
                Email = dto.Email,
                //Password = HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                UniversityName = dto.UniversityName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
             
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                // Handle errors (e.g., log them, throw an exception, etc.)
                return null;
            }

          
            await _userManager.AddToRoleAsync(user, "User");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user) {
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
           
            new Claim("name", user.Name)
        };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // This is required for [Authorize(Roles = "Admin")]
            }

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