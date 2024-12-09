using Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
            });
            services.AddAutoMapper(typeof(ServiceRegistration).Assembly);
            services.AddOptions<ApplicationOptions>().Bind(configuration.GetSection("ApplicationOptions"));
        }
    }
}
