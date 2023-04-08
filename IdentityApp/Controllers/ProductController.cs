using IdentityApp.DTOs.ProductDto;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result= await productService.GetProductListAsync();
       
            var response = result.Select(ProductResponse.FromProduct).ToList();

            return Ok(response);
       
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequest request)
        {
            await productService.CreateAsync(request);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTypes()
        {
            var result = await productService.GetTypeAsync();
            return Ok(result);
        }

    }
}
