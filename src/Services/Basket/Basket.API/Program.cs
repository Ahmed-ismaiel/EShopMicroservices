using BuildingBlocks.Behaviors;
using Carter;

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


var app = builder.Build();

//  Configure The HTTP request Pipeline

app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
