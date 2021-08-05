using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantAPI.Middleware;
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
            services.AddScoped<ErrorHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,RestaurantApiDbContext dbContext,
            IRestaurantRepository restaurantRepository)
        {
          
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var dataBase = dbContext;
            dataBase.Database.EnsureCreated();
            restaurantRepository.Seed();

        }
    }
}
