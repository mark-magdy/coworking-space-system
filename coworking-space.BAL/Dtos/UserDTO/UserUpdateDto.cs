using System.ComponentModel.DataAnnotations;

namespace coworking_space.BAL.Dtos.UserDTO
{
    public class UserUpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UniversityName { get; set; }
        public bool? IsActive { get; set; }
    }
}