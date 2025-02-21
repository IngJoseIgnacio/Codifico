using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;
using Api_SalesDatePrediction.Persistences;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Web.WebPages;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios personalizados
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Agregar servicios de controlador
builder.Services.AddControllers();

// Configuraci�n de Swagger con autenticaci�n
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api", Version = "v1" });

    // Configurar autenticaci�n en Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token de autenticaci�n JWT."
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configuraci�n del pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Api V1"));
}

app.UseHttpsRedirection();
app.UseRouting();

// Agregar CORS antes de Authentication y Authorization
app.UseCors("AllowReactApp");

app.UseAuthentication(); // Asegura que se aplique la autenticaci�n
app.UseAuthorization();

app.MapControllers();

app.Run();
