using System.ComponentModel.DataAnnotations;

namespace coworking_space.BAL.Dtos.UserDTO
{
    public class UserCreateDto
    {
       
        public string Name { get; set; }
        
        public string Email { get; set; }
       
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UniversityName { get; set; }
        public string? Image { get; set; } // URL or path to the user's image
    }
}
