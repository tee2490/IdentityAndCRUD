using IdentityApp.DTOs.ProductDto;

namespace IdentityApp.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext dataContext;

        public ProductService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<List<Product>> GetProductListAsync()
        {
            var result = await dataContext.Products
                .OrderByDescending(p=>p.Id).ToListAsync();
            return result;
        }

        public async Task CreateAsync(ProductRequest request)
        {
            var result = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description,
                Type = request.Type,
            };

            await dataContext.Products.AddAsync(result);
            await dataContext.SaveChangesAsync();
        }

        public async Task<List<string>> GetTypeAsync()
        {
            var result = await dataContext.Products.GroupBy(p => p.Type)
                .Select(result => result.Key).ToListAsync();
            return result;
        }

    }
}
