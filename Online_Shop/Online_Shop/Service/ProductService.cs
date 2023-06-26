using AutoMapper;
using Online_Shop.Dto;
using Online_Shop.Exceptions;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Interfaces.ServiceInterfaces;
using Online_Shop.Models;
using Online_Shop.Repository;
using System.Text;

namespace Online_Shop.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductService(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ProductDto> CreateProduct(int id, CreateProductDto productDto)
        {
            if (String.IsNullOrEmpty(productDto.Name) || String.IsNullOrEmpty(productDto.Amount.ToString()) ||
                String.IsNullOrEmpty(productDto.Price.ToString()) || String.IsNullOrEmpty(productDto.Description))
                throw new BadRequestException($"You must fill in all fields for adding product!");

            if(productDto.Price < 1 || productDto.Amount < 1)
                throw new BadRequestException($"Invalid field values!");

            List<User> users = await _userRepository.GetAll();
            User salesman = users.Where(s => s.Id == id).FirstOrDefault();

            if (salesman == null)
                throw new NotFoundException($"Salesman with ID: {id} doesn't exist.");

            Product newProduct = _mapper.Map<Product>(productDto);
            using(var memoryStream = new MemoryStream())
            {
                productDto.ImageForm.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                newProduct.Image = imageBytes;
            }

            newProduct.Deleted = false;
            newProduct.User = salesman;
            newProduct.UserId = id;

            ProductDto dto = _mapper.Map<Product, ProductDto>(await _productRepository.CreateProduct(newProduct));         
            return dto;
        }

        public async Task<bool> DeleteProduct(int userId, int productId)
        {
            List<Product> products = await _productRepository.GetAllProducts();
            products = products.Where(p=> p.UserId ==  userId).ToList();
            Product p = products.Where(p => p.Id == productId).FirstOrDefault();
            if (p == null)
                throw new NotFoundException($"Product with ID: {productId} doesn't exist.");
            return await _productRepository.DeleteProduct(productId);
        }

        public async Task<List<ProductDto>> GetAll()
        {
            List<Product> products = await _productRepository.GetAllProducts();
            products = products.Where(p => p.Deleted == false).ToList();

            return _mapper.Map<List<Product>, List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetMyProducts(int id)
        {
            List<Product> products = await _productRepository.GetAllProducts();
            products = products.Where(p => p.UserId == id && p.Deleted == false).ToList();
            if (products == null)
                throw new NotFoundException($"There are no products!");
            List<ProductDto> lista = _mapper.Map<List<Product>, List<ProductDto>>(products);
            return lista;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            Product p = await _productRepository.GetProductById(id);
            if (p == null)
                throw new NotFoundException($"Product with ID: {id} doesn't exist.");
            return _mapper.Map<Product, ProductDto>(p);
        }

        public async Task<ProductDto> UpdateProduct(int userId, int productId, UpdateProductDto productDto)
        {
            Product p = await _productRepository.GetProductById(productId);
            if (p == null)
                throw new NotFoundException($"Product with ID:{productId} doesn't exist.");

            if (p.UserId != userId)
                throw new BadRequestException($"You can't update this product!");

            if (String.IsNullOrEmpty(productDto.Name) || String.IsNullOrEmpty(productDto.Amount.ToString()) ||
                String.IsNullOrEmpty(productDto.Price.ToString()) || String.IsNullOrEmpty(productDto.Description))
                throw new BadRequestException($"You must fill in all fields for updating product!");

            if (productDto.Price < 1 || productDto.Amount < 1)
                throw new BadRequestException($"Invalid field values!");

            _mapper.Map(productDto, p);
            using (var memoryStream = new MemoryStream())
            {
                productDto.ImageForm.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                p.Image = imageBytes;
            }

            ProductDto dto = _mapper.Map<Product, ProductDto>(await _productRepository.UpdateProduct(p));
            return dto;
        }
    }
}
