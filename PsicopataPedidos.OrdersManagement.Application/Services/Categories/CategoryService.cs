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

namespace PsicopataPedidos.OrdersManagement.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> AddCategory(CategoryRequestDto categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            var result  = await _categoryRepository.AddAsync(category);

            return _mapper.Map<CategoryResponseDto>(result);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException(nameof(Category), id);

            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<CategoryResponseDto> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException(nameof(Category), id);

            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<IReadOnlyCollection<CategoryResponseDto>> ListAllCategories()
        {
            var categoryList = await _categoryRepository.ListAllAsync();
            var result = _mapper.Map<List<CategoryResponseDto>>(categoryList);

            return result;
        }

        public async Task<CategoryResponseDto> UpdateCategory(int id, CategoryRequestDto categoryRequest)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException(nameof(Category), id);

            _mapper.Map(categoryRequest, category, typeof(CategoryRequestDto), typeof(Category));

            var result = _categoryRepository.UpdateAsync(category);

            return _mapper.Map<CategoryResponseDto>(category);
        }
    }
}
