using AutoMapper;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Products
{
    public class ProductServiceMappingProfile : Profile
    {
        public ProductServiceMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<Category, ProductCategoryDto>();
            CreateMap<ProductRequestDto, Product>()
                .ForSourceMember(p => p.Categories, opt => opt.DoNotValidate());
        }
    }
}
