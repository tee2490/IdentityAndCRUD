using AutoMapper;
using IdentityApp.DTOs.ProductDto;

namespace IdentityApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IUploadFileService uploadFileService;
        private readonly IMapper mapper;
        private readonly DataContext dataContext;

        public ProductService(IUploadFileService uploadFileService, IMapper mapper, DataContext dataContext)
        {
            this.uploadFileService = uploadFileService;
            this.mapper = mapper;
            this.dataContext = dataContext;
        }
        public async Task<List<Product>> GetProductListAsync()
        {
            var result = await dataContext.Products.Include(p => p.ProductImages)
                .OrderByDescending(p=>p.Id).ToListAsync();

            return result;
        }

        public async Task<string> CreateAsync(ProductRequest request)
        {
            //อัพโหลดไฟล์
            (string errorMessage, List<string> imageNames) = await UploadImageAsync(request.FormFiles);
            if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;

            var result = mapper.Map<Product>(request);
            await dataContext.Products.AddAsync(result);
            await dataContext.SaveChangesAsync();

            //จัดการไฟล์ในฐานข้อมูล
            if (imageNames.Count > 0)
            {
                var images = new List<ProductImage>();
                foreach (var image in imageNames)
                {
                    images.Add(new ProductImage { ProductId = result.Id, Image = image });
                }
                await dataContext.ProductImages.AddRangeAsync(images);
            }

            await dataContext.SaveChangesAsync();

            return null;
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

        public async Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageNames = new List<string>();

            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    //บันทึกลงไฟล์ในโฟลเดอร์ 
                    imageNames = await uploadFileService.UploadImages(formFiles);
                }
            }
            return (errorMessage, imageNames);
        }

    }
}
