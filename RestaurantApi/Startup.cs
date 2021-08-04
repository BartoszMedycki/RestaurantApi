using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantApiDataBase;
using RestaurantApiDataBase.Interfaces;

namespace RestaurantApi
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
            services.AddDbContext<RestaurantApiDbContext>(options => options.UseSqlServer("Server = .;Database=RestaurantDb;Trusted_Connection=true;"));
            services.AddTransient<IRestaurantRepository, RestaurantApiDataBase.Repositories.RestaurantRepository>();
            services.AddTransient<RestaurantApiDataBase.Mappers.RestaurantMapper>();
            services.AddTransient<RestaurantApiDataBase.Mappers.CreateRestaurantMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,RestaurantApiDbContext dbContext,
            IRestaurantRepository restaurantRepository)
        {
            var dataBase = dbContext;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            dataBase.Database.EnsureCreated();
            restaurantRepository.Seed();
           
        }
    }
}
