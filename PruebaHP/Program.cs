using PruebaHP.Data;
using PruebaHP.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonajeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PersonajeContext")));


// 🔐 Clave secreta para firmar los tokens JWT
var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyWithAtLeast32Characters"); // 👈 Asegúrate que sea igual en AuthController

// 🔐 Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// 📘 Configuración de Swagger con soporte para JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPokemonListas", Version = "v1" });

    // Define el esquema de seguridad JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Introduce tu token JWT con el prefijo 'Bearer'. Ejemplo: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Aplica el esquema a todas las operaciones
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
            new List<string>()
        }
    });
});

// 🧱 Servicios MVC (controladores)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Swagger UI

var app = builder.Build();

// 🌐 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    // 🛡️ Autenticación JWT
app.UseAuthorization();     // 🔐 Reglas de autorización (como [Authorize])

app.MapControllers();

app.Run();