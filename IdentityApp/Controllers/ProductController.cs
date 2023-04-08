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
            var result = await productService.CreateAsync(request);

            if (result != null) return BadRequest(result);

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTypes()
        {
            var result = await productService.GetTypeAsync();
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest productRequest)
        {
            var result = await productService.GetByIdAsync((int)productRequest.Id);

            if (result == null) return NotFound();

            await productService.UpdateAsync(productRequest);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await productService.GetByIdAsync(id);

            if (result == null) return NotFound();

            await productService.DeleteAsync(result);

            return Ok(new { status = "Deleted", result });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchProduct([FromQuery] string name = "")
        {
            var result = (await productService.SearchAsync(name))
                .Select(ProductResponse.FromProduct).ToList();

            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await productService.GetByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(ProductResponse.FromProduct(result));
        }

    }
}
