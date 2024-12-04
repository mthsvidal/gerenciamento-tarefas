using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Models
{
    public class Comentario
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(3000)]
        public string Texto { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid TarefaId { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        public Tarefa Tarefa { get; set; }
        public Usuario Usuario { get; set; }
    }
}
