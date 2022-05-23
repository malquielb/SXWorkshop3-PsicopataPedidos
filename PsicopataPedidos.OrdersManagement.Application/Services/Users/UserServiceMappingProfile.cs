using AutoMapper;
using PsicopataPedidos.OrdersManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Users
{
    public class UserServiceMappingProfile : Profile
    {
        public UserServiceMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
