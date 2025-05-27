using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationNawatech.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        public string? Name { get; set; }

        public IFormFile? ProfilePicture { get; set; } // File upload
    }
}
