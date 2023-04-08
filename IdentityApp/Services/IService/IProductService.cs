
using IdentityApp.DTOs.ProductDto;

namespace IdentityApp.Services.IService
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
        Task<string> CreateAsync(ProductRequest request);
        Task<List<string>> GetTypeAsync();
        Task UpdateAsync(ProductRequest request);
        Task DeleteAsync(Product product);
        Task<List<Product>> SearchAsync(string name);
        Task<Product> GetByIdAsync(int id);
        Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles);
    }
}
