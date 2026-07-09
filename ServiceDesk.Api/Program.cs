using Microsoft.EntityFrameworkCore;
using ServiceDesk.Business.Services;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Data.Context;
using ServiceDesk.Data.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using ServiceDesk.Business.Exceptions;

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

app.UseAuthorization();

app.MapControllers();

app.Run();