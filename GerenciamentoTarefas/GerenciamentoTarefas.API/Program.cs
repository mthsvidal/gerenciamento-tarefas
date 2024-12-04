using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Models;
using GerenciamentoTarefas.Domain.Services;
using GerenciamentoTarefas.Infra.Database;
using GerenciamentoTarefas.Infra.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<GerenciamentoTarefasContext>(options =>
            options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBaseRepository<Projeto>, BaseRepository<Projeto>>();
builder.Services.AddScoped<IBaseRepository<Tarefa>, BaseRepository<Tarefa>>();
builder.Services.AddScoped<IBaseRepository<Comentario>, BaseRepository<Comentario>>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

builder.Services.AddScoped<ProjetoService>();
builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<ComentarioService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Configura o título e a versão da API
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gerenciamento de Tarefas",
        Version = "beta",
        Description = "Projeto-protótipo de gerenciamento de tarefas.",
        Contact = new OpenApiContact
        {
            Name = "Matheus Vidal",
            Email = "matheus_vidal@outook.com",
        }
    });

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "GerenciamentoTarefas.API.xml"));

});

var app = builder.Build();

// Executa as migrações e o seed após a inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GerenciamentoTarefasContext>();

    // Executa as migrações pendentes
    context.Database.Migrate();

    SeedDatabase.SeedUsuario(context);
}

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
