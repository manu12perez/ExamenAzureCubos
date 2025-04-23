using ExamenAzureCubos.Data;
using ExamenAzureCubos.Helpers;
using ExamenAzureCubos.Repositories;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

/***********************************************************************************************************/
HelperActionServicesOAuth helper = new HelperActionServicesOAuth(builder.Configuration);
builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);
builder.Services.AddAuthentication(helper.GetAuthenticateSchema()).AddJwtBearer(helper.GetJwtBearerOptions());
HelperCryptography.Initialize(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient(builder.Configuration.GetSection("KeyVault"));
});
SecretClient secretClient = builder.Services.BuildServiceProvider().GetService<SecretClient>();
KeyVaultSecret secret = await secretClient.GetSecretAsync("SqlAzure");
/***********************************************************************************************************/

// Add services to the container.
/***********************************************************************************************************/
//string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
string connectionString = secret.Value;
builder.Services.AddDbContext<CubosContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<RepositoryCubos>();
/***********************************************************************************************************/

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

/***********************************************************************************************************/
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Api Seguridad Cubos");
    options.RoutePrefix = "";
});
/***********************************************************************************************************/

app.UseHttpsRedirection();

/***********************************************************************************************************/
app.UseAuthentication();
/***********************************************************************************************************/
app.UseAuthorization();

app.MapControllers();

app.Run();
