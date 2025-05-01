using coworking_space.BAL.DTOs.OrderDTO;
using System.ComponentModel.DataAnnotations;

namespace coworking_space.BAL.Dtos.UserDTO
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UniversityName { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public List<OrderReadDto>? Orders { get; set; } 
    }
}