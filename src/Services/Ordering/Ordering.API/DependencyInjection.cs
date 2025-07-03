namespace Ordering.API
{
    public  static class DependencyInjection
    {

        // This method is used to register API services in the dependency injection container.
        // It can be called from the Program.cs file to set up the necessary services for the API layer.
        // nfs l fekra lly f Application hena n3ml extension method bgm3 feha kol l dependency injection registrations for the API layer.
        // 3ashan a3mmlha Register program.cs bta3i

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Register API services here
            // Example: services.AddControllers();
            // services.AddEndpointsApiExplorer();
            // services.AddSwaggerGen();
            // Register application and infrastructure services
            // services.AddApplicationServices();
            //services.AddInfrastructureServices();
            //services.AddCarter();
            return services;
        }


        // This method is used to configure the HTTP request pipeline for the API.
        // It can be called from the Program.cs file to set up middleware and routing for the API layer.
        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure API services here
            // Example: app.UseSwagger();
            // app.UseSwaggerUI();
            // app.UseRouting();
            // app.UseAuthorization();
            // app.MapControllers();
            // app.MapCarter();
            return app;
        }


    }
}
