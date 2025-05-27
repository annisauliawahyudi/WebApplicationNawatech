using System.ComponentModel.DataAnnotations;

namespace WebApplicationNawatech.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public bool IsDeleted { get; set; } = false; // untuk soft delete
    }
}
