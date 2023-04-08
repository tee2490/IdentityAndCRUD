
using IdentityApp.DTOs.ProductDto;

namespace IdentityApp.Services.IService
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
        Task CreateAsync(ProductRequest request);
        Task<List<string>> GetTypeAsync();
    }
}
