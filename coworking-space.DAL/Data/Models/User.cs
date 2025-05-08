using coworking_space.DAL.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CO_Working_Space
{
    public enum UserStatus
    {
        Active,
        Inactive,
        Suspended
    }

    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        public string? Image { get; set; } // URL or path to the user's image
        

        //[Required(ErrorMessage = "Confirm Password is required.")]
        //[DataType(DataType.Password)]

        //[Compare("Password", ErrorMessage = "Passwords do not match.")]

        //public string ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string? PhoneNumber { get; set; }

        public string ? UniversityName { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public string Role { get; set; } 



        // Navigation properties
        public virtual ICollection<Order>? Orders { get; set; } 
        public virtual ICollection<TotalReservations> ?TotalReservations { get; set; }

    }

}
