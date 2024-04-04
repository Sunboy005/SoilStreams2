using System.ComponentModel.DataAnnotations;

namespace SOILSTREAMAPI.Models.Dto
{
    public class UserRegistrationDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password field cannot be empty")]
        [MinLength(6, ErrorMessage = "Password cannot be less than 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Compare("Password", ErrorMessage = "Does not match password")]
        public string ConfirmPassword { get; set; }
    }
}
