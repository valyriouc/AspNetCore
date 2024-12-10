using System.Reflection;
using LearningPlatform;
using RepoDb;

var builder = WebApplication.CreateBuilder(args);

CancellationTokenSource cts = new CancellationTokenSource();

var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "myconfig.json");
await AppConfig.LoadAsync(configPath, cts.Token);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

GlobalConfiguration.Setup().UseSqlite();


app.UseHttpsRedirection();

app.MapDefaultControllerRoute();
app.Run();
