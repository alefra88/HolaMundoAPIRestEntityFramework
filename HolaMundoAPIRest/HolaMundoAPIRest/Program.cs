using HolaMundoAPIRest.Models.Context;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
//Agregar permisos CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:44313").AllowAnyMethod().AllowAnyHeader();
                      });
});
//Agregar referencia para la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("InstitutoTich");
builder.Services.AddDbContext<EstadosContext>(x => x.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
