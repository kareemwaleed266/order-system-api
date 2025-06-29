using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.Services.ProductService;
using Order.Service.Services.ProductService.ProductDto;

namespace Order.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductResultDto>> AddProduct(ProductDetailsDto input)
        {
            var product = await _productService.AddProductAsync(input);

            if (product is null)
                return BadRequest(("Error on Adding Product"));

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<ProductResultDto>> GetAllProductsDetails()
            => Ok(await _productService.GetAllProductsDetailsAsync());

        [HttpGet]
        public async Task<ActionResult<ProductResultDto>> GetProductDetailsById(int? id)
            => Ok(await _productService.GetProductDetailsByIdAsync(id));


        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductResultDto>> UpdateProduct(int? id, ProductDetailsDto input)
            => Ok(await _productService.UpdateProductAsync(id, input));
    }
}
