using Microsoft.EntityFrameworkCore;
using ServiceDesk.Business.Services;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Data.Context;
using ServiceDesk.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<ICategoriaService, CategoriaService>();


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();