using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using OauthPlayground;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<ReverseMiddleware>();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.ForwardChallenge = GoogleDefaults.AuthenticationScheme;
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Google:ClientId"];
        options.ClientSecret = builder.Configuration["Google:ClientSecret"];
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<ReverseMiddleware>();
app.UseAuthorization();
app.UseEndpoints(builder => builder.MapDefaultControllerRoute());

app.UseSpaStaticFiles();
app.UseSpa(configuration: builder =>
{
    if (app.Environment.IsDevelopment())
    {
        builder.UseProxyToSpaDevelopmentServer("http://localhost:8080/");
    }
});

app.Run();
