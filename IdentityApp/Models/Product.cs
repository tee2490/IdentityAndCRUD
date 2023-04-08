using System.Text.Json.Serialization;

namespace IdentityApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public int QuantityInStock { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public List<ProductImage> ProductImages { get; set; }
    }
}
