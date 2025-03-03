var builder = WebApplication.CreateBuilder(args);

// Add Services to the Container


var app = builder.Build();

//  Configure The HTTP request Pipeline

app.MapGet("/", () => "Hello World!");

app.Run();
