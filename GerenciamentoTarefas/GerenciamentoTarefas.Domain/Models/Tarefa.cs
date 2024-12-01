using GerenciamentoTarefas.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        [Required]
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;

        [Required]
        public PrioridadeTarefa Prioridade { get; set; }

        [Required]
        public Guid ProjetoId { get; set; } // Relacionamento com o projeto
        public Projeto Projeto { get; set; }

        // Relacionamento
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<HistoricoAtualizacao> Historicos { get; set; } = new List<HistoricoAtualizacao>();
    }
}
