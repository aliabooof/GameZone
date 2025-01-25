

using GameZone1.Services;

namespace GameZone.Extensions
{
    public static class ServicesAndConfigurations
    {
        public static IServiceCollection LoadServicesAndConfigs(this IServiceCollection services,
          IConfiguration config)
        {
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            //         ?? throw new InvalidOperationException("no connection string was found");

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(connectionString));
            //// Add services to the container.

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICategoriesService, CategoriesService>();

            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
