using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Identity;
using Notes.Identity.Data;
using Notes.Identity.Models;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration["DbConnection"];

// Register MySql
builder.Services.AddDbContext<AuthDbContext>(b =>
{
    b.UseMySql(connString, ServerVersion.AutoDetect(connString));
});

// Register controllers
builder.Services.AddControllersWithViews();

// Register Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Notes.Identity.Cookie";
    options.LoginPath = "/auth/signin";
    options.LogoutPath = "/auth/signout";
});

// Register IdentityServer4
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<User>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Database
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AuthDbContext>();
        DbInitializer.Initialize(context);
    }
    catch
    {
        throw;
    }
}

app.UseRouting();

// IdentityServer4 (OAuth 2.0 & OpenID Connect)
app.UseIdentityServer();

// Endpoints
app.UseEndpoints(b =>
{
    b.MapDefaultControllerRoute();
});

app.Run();
