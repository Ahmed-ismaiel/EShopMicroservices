
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

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

// configure validators from another assembly

builder.Services.AddValidatorsFromAssembly(assembly);



// configure Marten and use lightweight sessions for Marten and give the connection string

builder.Services.AddMarten(opts =>
{

   opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

// Hena hn3ml seeding ll data bt3tna 
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Default Health check  then use it for postgresql

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();


// Configure Custom exception handler
app.UseExceptionHandler(options => { });


// Configure the health check endpoint 
// and Get data in json formate

app.UseHealthChecks("/health",

    new HealthCheckOptions { 
    
    
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    
    
    
    });


// Configure the exception handler The exception handler
// is a middleware that catches exceptions and returns a ProblemDetails object with the exception details.

#region Normal Validation
//app.UseExceptionHandler(exceptionHandelerApp =>
//{


//    exceptionHandelerApp.Run(async context =>
//    {

//        var exeption = context.Features.Get<IExceptionHandlerFeature>()?.Error;

//        if (exeption is null)
//        {
//            return;
//        }

//        var promblemDetails = new ProblemDetails
//        {
//            Title = exeption.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exeption.StackTrace
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exeption, exeption.Message);


//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(promblemDetails);








//    });
//}); 
#endregion

app.Run();
