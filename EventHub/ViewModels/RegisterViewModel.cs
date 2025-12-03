using System.ComponentModel.DataAnnotations;

namespace EventHub.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        public string UserType { get; set; } = "Customer";
    }
}
