using Microsoft.EntityFrameworkCore;
using ServiceDesk.Business.Services;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Data.Context;
using ServiceDesk.Data.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using ServiceDesk.Business.Exceptions;
using Microsoft.AspNetCore.Identity;
using ServiceDesk.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("A chave JWT não foi configurada.");
}
// Controllers
builder.Services.AddControllers();

// Banco de dados SQL Server
builder.Services.AddDbContext<ServiceDeskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<ChamadoRepository>();
builder.Services.AddScoped<AtendimentoRepository>();

// Services com interfaces
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<IAtendimentoService, AtendimentoService>();
builder.Services.AddScoped<CategoriaRepository>();

// Password Hasher
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Informe o token JWT no formato: Bearer SEU_TOKEN_AQUI",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();


// Swagger em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature?.Error;

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            RegraDeNegocioException => StatusCodes.Status400BadRequest,
            RecursoNaoEncontradoException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var mensagem = exception is null
            ? "Ocorreu um erro inesperado."
            : exception.Message;

        await context.Response.WriteAsJsonAsync(new
        {
            mensagem
        });
    });
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();