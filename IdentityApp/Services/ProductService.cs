using AutoMapper;
using IdentityApp.DTOs.ProductDto;

namespace IdentityApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly DataContext dataContext;

        public ProductService(IMapper mapper, DataContext dataContext)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;
        }
        public async Task<List<Product>> GetProductListAsync()
        {
            var result = await dataContext.Products.Include(p => p.ProductImages)
                .OrderByDescending(p=>p.Id).ToListAsync();

            return result;
        }

        public async Task CreateAsync(ProductRequest request)
        {
            var result = mapper.Map<Product>(request);

            await dataContext.Products.AddAsync(result);
            await dataContext.SaveChangesAsync();
        }

        public async Task<List<string>> GetTypeAsync()
        {
            var result = await dataContext.Products.GroupBy(p => p.Type)
                .Select(result => result.Key).ToListAsync();
            return result;
        }

        public async Task UpdateAsync(ProductRequest request)
        {
            var result = mapper.Map<Product>(request);

            dataContext.Products.Update(result);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            dataContext.Products.Remove(product);
            await dataContext.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchAsync(string name)
        {
            var result = await dataContext.Products.Include(p => p.ProductImages)
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
            return result;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var result = await dataContext.Products.Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return result;
        }

    }
}
