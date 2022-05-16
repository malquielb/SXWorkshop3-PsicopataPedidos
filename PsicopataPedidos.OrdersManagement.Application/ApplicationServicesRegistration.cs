using Microsoft.Extensions.DependencyInjection;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Services.Categories;
using PsicopataPedidos.OrdersManagement.Application.Services.Orders;
using PsicopataPedidos.OrdersManagement.Application.Services.Products;
using PsicopataPedidos.OrdersManagement.Application.Services.ShoppingList;
using PsicopataPedidos.OrdersManagement.Application.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShoppingListService, ShoppingListService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
