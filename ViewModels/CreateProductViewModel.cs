using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationNawatech.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }

        public IFormFile? ImageFile { get; set; } // Untuk upload gambar
    }
}
