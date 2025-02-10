
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
});

// configure validators from another assembly

builder.Services.AddValidatorsFromAssembly(assembly);



// configure Marten and use lightweight sessions for Marten and give the connection string

builder.Services.AddMarten(opts =>
{

   opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();




var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.Run();
