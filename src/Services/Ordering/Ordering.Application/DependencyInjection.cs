using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        // This method is used to register application services in the dependency injection container.
        // It can be called from the API project to set up the necessary services for the application layer.
        // l fekra hena any b3ml extension method bgm3 feha kol l dependency injection registrations for the application layer.
        // 3ashan a3mmlha Register program.cs bta3i
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            // Example: services.AddScoped<IOrderService, OrderService>();

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }

    }
}
