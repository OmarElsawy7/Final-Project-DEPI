using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventHub.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string? CurrentPhotoPath { get; set; }

        public IFormFile? NewPhoto { get; set; }
    }
}
