var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

// configure carter
builder.Services.AddCarter();


// configure mediatr from another assembly
builder.Services.AddMediatR(configuration =>
{

    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// configure Marten and use lightweight sessions for Marten and give the connection string

builder.Services.AddMarten(opts =>
{

   opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();




var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.Run();
