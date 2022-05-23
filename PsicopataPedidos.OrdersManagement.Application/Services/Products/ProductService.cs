using AutoMapper;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Exceptions;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ProductRequestDtoValidator _validator;

        public ProductService(IProductRepository productRepository, IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validator = new ProductRequestDtoValidator();
        }

        public async Task<ProductResponseDto> AddProduct(ProductRequestDto productRequest)
        {
            var validationResult = await _validator.ValidateAsync(productRequest);

            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

            var product = _mapper.Map<Product>(productRequest);
            product.Categories = new List<Category>();

            foreach (var categoryId in productRequest.Categories)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);

                if (category == null)
                    throw new NotFoundException(nameof(Category), categoryId);

                product.Categories.Add(category);
            }

            var result  = await _productRepository.AddAsync(product);

            return _mapper.Map<ProductResponseDto>(result);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException(nameof(Product), id);

            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductResponseDto> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException(nameof(Product), id);

            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<List<ProductResponseDto>> ListAllProducts()
        {
            var productList = await _productRepository.ListAllAsync();
            var result = _mapper.Map<List<ProductResponseDto>>(productList);

            return result;
        }

        public async Task<ProductResponseDto> UpdateProduct(int id, ProductRequestDto productRequest)
        {
            var validationResult = await _validator.ValidateAsync(productRequest);

            if (validationResult.Errors.Any())
                throw new ValidationException(validationResult);

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException(nameof(Category), id);

            _mapper.Map(productRequest, product, typeof(ProductRequestDto), typeof(Product));

            var result = _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductResponseDto>(product);
        }
    }
}
