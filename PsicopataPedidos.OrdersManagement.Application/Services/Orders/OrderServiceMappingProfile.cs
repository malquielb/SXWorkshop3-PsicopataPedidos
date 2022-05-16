using AutoMapper;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Orders
{
    public class OrderServiceMappingProfile : Profile
    {
        public OrderServiceMappingProfile()
        {
            CreateMap<Order, OrderResponseDto>()
                .ForMember(order => order.UserName, opt => opt.Ignore());
            CreateMap<ShoppingListItem, OrderShoppingListItemResponseDto>();
            CreateMap<Product, OrderListProductResponseDto>();
        }
    }
}
