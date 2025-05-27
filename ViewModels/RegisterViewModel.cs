using System.ComponentModel.DataAnnotations;

namespace WebApplicationNawatech.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="Password minimal 6 karakter")]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
