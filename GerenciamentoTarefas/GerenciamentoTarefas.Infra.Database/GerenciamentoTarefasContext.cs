using GerenciamentoTarefas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Infra.Database
{
    public class GerenciamentoTarefasContext : DbContext
    {
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<HistoricoAtualizacao> HistoricosAtualizacao { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        public GerenciamentoTarefasContext(DbContextOptions<GerenciamentoTarefasContext> options) : base(options)
        {

        }
    }
}
