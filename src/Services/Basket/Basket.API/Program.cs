using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the Container

// configure carter
builder.Services.AddCarter();

// configure mediatr from another assembly
//Refactoring the code to use the AddMediatR method to register the services
//from the assembly that contains the handlers.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(configuration =>
{

    configuration.RegisterServicesFromAssembly(assembly);
    // hena 3mlt confiure lel global behavior pipeline 3shan y3ml validation lel request 
    // abl ma y3ml handle
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));



});

// congigure the Basket repository

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// msh hynf3 a3ml Register l IBasketRepository Aktr mn mara 3ashan akhr 7aga hya ly htshtghl 
// 3shan kda nzlt scurtor owe astkhdmt l extension method bta3ha  

builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// configure the distributed cache 

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket";

});

// Configure Grpc Client for Discount Service

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    // Configure the handler to use the default HTTP client handler
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

// configure Marten and use lightweight sessions for Marten and give the connection string

builder.Services.AddMarten(opts =>
{

    opts.Connection(builder.Configuration.GetConnectionString("Database")!);

    // Hena ana b2olo en el identity bta3 el shopping cart hya el username
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

// bstkhdm l custome exception handler middleware 3asshan y3ml handle lel exceptions

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add Health Checks to know if my code is running or not
// then add health check for the database and redis

builder.Services.AddHealthChecks()
    .AddRedis(
        builder.Configuration.GetConnectionString("Redis")!)
    .AddNpgSql(
        builder.Configuration.GetConnectionString("Database")!);



var app = builder.Build();

//  Configure The HTTP request Pipeline

app.MapCarter();
// Configure Custom exception handler
app.UseExceptionHandler(options => { });

// Expose health check endpoint
app.MapHealthChecks("/health" ,

    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    
   

app.MapGet("/", () => "Hello World!");

app.Run();
