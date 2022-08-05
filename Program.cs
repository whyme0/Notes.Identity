using IdentityServer4.Models;

var builder = WebApplication.CreateBuilder(args);

// Register IdentityServer4
builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(new List<ApiResource>())
    .AddInMemoryIdentityResources(new List<IdentityResource>())
    .AddInMemoryApiScopes(new List<ApiScope>())
    .AddInMemoryClients(new List<Client>())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseRouting();

// IdentityServer4 (OAuth 2.0 & OpenID Connect)
app.UseIdentityServer();

app.Run();
