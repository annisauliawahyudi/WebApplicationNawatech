using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApplicationNawatech.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

    
        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public int? ProductCategoryId { get; set; }

        public IFormFile? Image { get; set; } // Untuk upload gambar baru

        public string? ExistingImageUrl { get; set; } // Menyimpan gambar lama
    }
}
