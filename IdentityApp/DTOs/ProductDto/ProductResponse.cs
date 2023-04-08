﻿
namespace IdentityApp.DTOs.ProductDto
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public int QuantityInStock { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrls { get; set; }

        static public ProductResponse FromProduct(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                QuantityInStock = product.QuantityInStock,
                ImageUrls = product.ProductImages.Select(x => x.Image).ToList()
            };
        }

    }
}
