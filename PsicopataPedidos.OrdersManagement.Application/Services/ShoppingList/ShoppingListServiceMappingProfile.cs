using AutoMapper;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList
{
    public class ShoppingListServiceMappingProfile : Profile
    {
        public ShoppingListServiceMappingProfile()
        {
            CreateMap<ShoppingListItemRequestDto, ShoppingListItem>();
            CreateMap<ShoppingListItem, ShoppingListItemResponseDto>();
            CreateMap<Product, ListProductResponseDto>();
        }
    }
}
