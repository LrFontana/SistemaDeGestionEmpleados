using LibreriaBase.Entidades;
using LibreriaServer.Datos;
using LibreriaServer.Helpers;
using LibreriaServer.Repositorios.Contratos;
using LibreriaServer.Repositorios.Implementaciones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Jwt
builder.Services.Configure<SeccionJwt>(builder.Configuration.GetSection("SeccionJwt"));
var jwtSeccion = builder.Configuration.GetSection(nameof(SeccionJwt)).Get<SeccionJwt>();

// Servicios.
// DbContext
builder.Services.AddDbContext<AppDbContext>(oprions =>
{
    oprions.UseSqlServer(builder.Configuration.GetConnectionString("MiConexion")?? throw new InvalidOperationException("Disculpa, tu conexion no fue encontrada."));
});

// Autenticaciones.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSeccion!.Issuer,
        ValidAudience = jwtSeccion.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSeccion.Key!))
    };
});

// Repositorio usario.
builder.Services.AddScoped<ICuentaUsuario, CuentaUsuarioRepositorio>();

// Repositorio Depto. General / Depto / Rama
builder.Services.AddScoped<IRepositorioGenerico<DepartamentoGeneral>, RepositorioDepartamentoGeneral>();
builder.Services.AddScoped<IRepositorioGenerico<Departamento>, DepartamentoRepositorio>();
builder.Services.AddScoped<IRepositorioGenerico<Rama>, RamaRepositorio>();

// Repositorio Pais / Ciudad / Pueblo.
builder.Services.AddScoped<IRepositorioGenerico<Pais>, PaisRepositorio>();
builder.Services.AddScoped<IRepositorioGenerico<Ciudad>, CiudadRepositorio>();
builder.Services.AddScoped<IRepositorioGenerico<Pueblo>, PubloRepositorio>();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        builder => builder
        .WithOrigins("https://localhost:5108", "https://localhost:7059")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorWasm");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
