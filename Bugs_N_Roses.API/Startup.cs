using AutoMapper;
using Bugs_N_Roses.Application.AutoMapper;
using Bugs_N_Roses.Application.Extensions;
using Bugs_N_Roses.Application.Services.AuthServices;
using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugs_N_Roses.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();


            services.AddAutoMapper(x => x.AddProfile(typeof(AutoMapperConfiguration)));

            services.AddScoped<IAuthService, AuthService>();
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMapperConfiguration());
            //});
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            //services.AddDefaultIdentity<User>()
            //    .AddEntityFrameworkStores<HW4DBContext>()
            //    .AddDefaultTokenProviders();

            //services.AddDefaultIdentity<IdentityUser<int>>(opt => opt.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<HW4DBContext>();
            services.AddDefaultIdentity<User>()
                  .AddDefaultTokenProviders()
                  .AddEntityFrameworkStores<HW4DBContext>();

            services.AddApplicationModule(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bugs_N_Roses.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bugs_N_Roses.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
