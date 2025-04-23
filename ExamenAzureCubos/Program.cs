using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ExamenAzureCubos.Data;
using ExamenAzureCubos.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- Configuración Key Vault ----------
string keyVaultUrl = "https://keyvaultcubosmpb.vault.azure.net/";
var credential = new DefaultAzureCredential();
var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);

// Recuperar el secreto sin async/await
KeyVaultSecret secret = secretClient.GetSecret("SqlAzure");
string connectionString = secret.Value;

// ---------- Configuración de servicios ----------
builder.Services.AddSingleton(secretClient);
builder.Services.AddDbContext<CubosContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ---------- Middleware ----------
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Api Cubos");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
