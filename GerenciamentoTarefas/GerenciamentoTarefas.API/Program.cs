using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Models;
using GerenciamentoTarefas.Domain.Services;
using GerenciamentoTarefas.Infra.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<GerenciamentoTarefasContext>(options =>
            options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBaseRepository<Projeto>, BaseRepository<Projeto>>();

builder.Services.AddScoped<ProjetoService>();

//builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
