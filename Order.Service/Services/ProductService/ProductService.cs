using AutoMapper;
using Order.Data.Entities.ProductEntities;
using Order.Repository.Interfaces;
using Order.Service.Services.ProductService.ProductDto;

namespace Order.Service.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<ProductResultDto> AddProductAsync(ProductDetailsDto input)
        {
            if (input == null) throw new Exception("Your Input Not Valid");

            var productExist = await _unitOfWork.Repository<Product, int>().ExistsAsync(x => x.Name == input.Name);
            if (productExist)
                throw new Exception("Product Already Exists");

            var mappedProduct = _mapper.Map<Product>(input);

            await _unitOfWork.Repository<Product, int>().AddAsync(mappedProduct);
            await _unitOfWork.CompleteAsync();

            var mappedInput = _mapper.Map<ProductResultDto>(mappedProduct);

            return mappedInput;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsDetailsAsync()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);

            return mappedProducts;
        }
        public async Task<ProductDetailsDto> GetProductDetailsByIdAsync(int? id)
        {
            if (id is null) throw new Exception("Id Is Null");

            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id.Value);

            if (product is null) throw new Exception("Product Not Found");
            var mappedproduct = _mapper.Map<ProductDetailsDto>(product);

            return mappedproduct;
        }

        public async Task<ProductResultDto> UpdateProductAsync(int? id, ProductDetailsDto input)
        {
            var productExist = await _unitOfWork.Repository<Product, int>().ExistsAsync(x => x.Id == id);
            if (!productExist)
                throw new Exception("Product Not Found");

            var mappedProduct = _mapper.Map<Product>(input);

            mappedProduct.Id = id.Value;

            _unitOfWork.Repository<Product, int>().Update(mappedProduct);
            await _unitOfWork.CompleteAsync();

            var mappedInput = _mapper.Map<ProductResultDto>(mappedProduct);

            return mappedInput;
        }
    }
}
