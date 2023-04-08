using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

