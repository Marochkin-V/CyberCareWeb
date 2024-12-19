using Microsoft.EntityFrameworkCore;
using CyberCareWeb.Infrastructure;
using CyberCareWeb.Infrastructure.Repositories;
using CyberCareWeb.Domain.Abstractions;

namespace CyberCareWeb.Web.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnection"), b =>
                b.MigrationsAssembly("CyberCareWeb.Infrastructure")));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
		services.AddScoped<IComponentTypeRepository, ComponentTypeRepository>();
		services.AddScoped<IComponentRepository, ComponentRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IOrderComponentRepository, OrderComponentRepository>();
    }
}
