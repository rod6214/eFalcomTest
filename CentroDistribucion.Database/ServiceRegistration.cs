using CentroDistribucion.Database.Contexts;
using CentroDistribucion.Database.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Domain.Services;
using CentroDistribucion.Database.Implementations;
using MediatR;

namespace CentroDistribucion.Database
{
    public static class ServiceRegistration
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddOptions<CentroDistribucionOption>().Bind(configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<CentroDistribucionContext>();
            services.AddTransient<ICentroDistribucionService, CentroDistribucionRepository>();
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
            });

        }
    }
}
