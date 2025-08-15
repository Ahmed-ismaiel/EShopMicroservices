using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


//-------------------
//Infrastructure - EF Core
//Application _ MediarR
//API - Carter , HealthChecks

// Register services for the application, infrastructure, and API layers
// Hena bst5dm extension methods ly f kol layer 3ashan a3ml registration l kol services bta3ty
builder.Services
 .AddApplicationServices()
 .AddInfrastructureServices(builder.Configuration)
 .AddApiServices();
//-------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}


app.Run();
