using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Models
{
    public class HistoricoAtualizacao
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Modificacoes { get; set; }

        [Required]
        public DateTime DataModificacao { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid TarefaId { get; set; }
        public Tarefa Tarefa { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        public required Usuario Usuario { get; set; }
    }

}
