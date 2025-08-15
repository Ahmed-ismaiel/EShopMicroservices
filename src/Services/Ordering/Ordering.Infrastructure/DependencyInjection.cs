using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {

        // This method is used to register infrastructure services in the dependency injection container.
        // nfs l fekra lly f Application hena n3ml extension method bgm3 feha kol l dependency injection registrations for the infrastructure layer.
        // 3ashan a3mmlha Register program.cs bta3i
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
            , IConfiguration configuration)
        {
            // Register infrastructure services here
            var connectionString = configuration.GetConnectionString("Database");
            // Example: services.AddScoped<IOrderRepository, OrderRepository>();\
            services.AddDbContext<ApplicationDbContext>(Options =>
            Options.UseSqlServer(connectionString));
            return services;
        }



    }
}
