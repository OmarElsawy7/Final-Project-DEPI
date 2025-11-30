using System.ComponentModel.DataAnnotations;

namespace EventHub.ViewModels
{
    public class RegisterViewModel
    {
        public required string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public required string UserType { get; set; }
    }
}
