var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddProjectService();

var app = builder.Build();

await app.Startup();

app.Run();
