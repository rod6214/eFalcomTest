using CentroDistribucion.Database.Contexts;
using CentroDistribucion.Database.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Services;
using CentroDistribucion.Database.Implementations;


namespace CentroDistribucion.Database
{
    public static class ServiceRegistration
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddOptions<CentroDistribucionOption>().Bind(configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<CentroDistribucionContext>();
            services.AddTransient<ICentroDistribucionService, CentroDistribucionRepository>();
        }
    }
}
