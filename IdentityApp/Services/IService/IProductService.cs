
namespace IdentityApp.Services.IService
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
    }
}
