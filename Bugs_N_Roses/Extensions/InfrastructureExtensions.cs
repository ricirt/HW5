using Bugs_N_Roses.Domain.Repositories;
using Bugs_N_Roses.Infrastructure.Context;
using Bugs_N_Roses.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<HW4DBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
            

            return services;
        }
    }
}
