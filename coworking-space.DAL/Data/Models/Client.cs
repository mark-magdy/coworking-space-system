using System.ComponentModel.DataAnnotations;

namespace CO_Working_Space
{

    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        public string UniversityName { get; set; }

        public bool IsActive { get; set; }
    }

}
