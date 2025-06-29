using Order.Service.Services.ProductService.ProductDto;

namespace Order.Service.Services.ProductService
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsDetailsAsync();
        Task<ProductDetailsDto> GetProductDetailsByIdAsync(int? id);
        Task<ProductResultDto> AddProductAsync(ProductDetailsDto input);
        Task<ProductResultDto> UpdateProductAsync(int? id, ProductDetailsDto input);
    }
}