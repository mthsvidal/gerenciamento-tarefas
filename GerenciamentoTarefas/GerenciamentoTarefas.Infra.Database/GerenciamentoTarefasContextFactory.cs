using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GerenciamentoTarefas.Infra.Database
{
    public class GerenciamentoTarefasContextFactory : IDesignTimeDbContextFactory<GerenciamentoTarefasContext>
    {
        public GerenciamentoTarefasContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GerenciamentoTarefasContext>();

            // Caminho para a pasta raiz
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "GerenciamentoTarefas.API");

            // Configurar o ConfigurationBuilder para carregar appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Define o diretório base como GerenciamentoTarefas.API
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Carregar o arquivo principal
                .AddJsonFile("appsettings.Development.json", optional: true) // Carregar o arquivo de desenvolvimento (se existir)
                .Build();

            GerenciamentoTarefasContextConfigurer.Configure(builder, configuration.GetConnectionString("Default"));

            return new GerenciamentoTarefasContext(builder.Options);
        }
    }
}
