using ExamenAzureCubos.Data;
using ExamenAzureCubos.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/****************************************************************************************************/
// Configuración de SQL Server
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddDbContext<CubosContext>(options => options.UseSqlServer(connectionString));

// Registro de repositorios
builder.Services.AddTransient<RepositoryCubos>();
/****************************************************************************************************/


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

/****************************************************************************************************/
app.MapOpenApi();
app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Api Cubos");
        options.RoutePrefix = "";
    });
/****************************************************************************************************/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
