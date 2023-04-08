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
            var result = await dataContext.Products.ToListAsync();
            return result;
        }
    }
}
