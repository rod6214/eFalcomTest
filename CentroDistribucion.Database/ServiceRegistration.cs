using CentroDistribucion.Database.Contexts;
using CentroDistribucion.Database.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroDistribucion.Database
{
    public static class ServiceRegistration
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<CentroDistribucionOption>(c => configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<CentroDistribucionContext>();
        }
    }
}
