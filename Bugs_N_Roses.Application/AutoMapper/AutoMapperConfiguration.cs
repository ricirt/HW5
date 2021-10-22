using AutoMapper;
using Bugs_N_Roses.Application.Models.OrderDetailModels;
using Bugs_N_Roses.Application.Models.OrderModels;
using Bugs_N_Roses.Application.Models.ProductModels;
using Bugs_N_Roses.Application.Models.UserModels;
using Bugs_N_Roses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.AutoMapper
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();

            CreateMap<User,LoginDto>().ReverseMap();
        }
    }
}
