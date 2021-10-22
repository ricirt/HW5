using Bugs_N_Roses.Application.Services.AuthServices;
using Bugs_N_Roses.Application.Services.OrderDetailServices;
using Bugs_N_Roses.Application.Services.OrderServices;
using Bugs_N_Roses.Application.Services.ProductServices;
using Bugs_N_Roses.Application.Services.UserServices;
using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Infrastructure.Context;
using Bugs_N_Roses.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Extensions
{
    public static class ApplicationModuleExtension
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureModule(configuration);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
