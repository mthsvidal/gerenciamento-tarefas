using GerenciamentoTarefas.Domain.Enumerables;
using GerenciamentoTarefas.Domain.Models;

namespace GerenciamentoTarefas.Infra.Database
{
    public static class SeedDatabase
    {
        public static void SeedUsuario(GerenciamentoTarefasContext context)
        {
            // Verifica se o banco de dados já possui dados
            if (!context.Usuarios.Any())
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Joana Silva",
                        Email = "joana.silva@teste.com",
                        Username = "joana.silva",
                        Cargo = Cargos.Gerente
                    },
                    new Usuario
                    {
                       Id = Guid.NewGuid(),
                        Nome = "Matheus Vidal",
                        Email = "matheus.vidal@teste.com",
                        Username = "matheus.vidal",
                        Cargo = Cargos.Analista
                    },
                    new Usuario
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Lucas Santos",
                        Email = "Lucas Santos@teste.com",
                        Username = "lucas.santos",
                        Cargo = Cargos.Analista
                    }
                };

                // Adiciona todos os usuários ao banco
                context.Usuarios.AddRange(usuarios);

                // Salva as alterações no banco de dados
                context.SaveChanges();
            }
        }

    }
}
