using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.DTOs
{
    public class ComentarioInputDto
    {
        public string Texto { get; set; }

        public Guid TarefaId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
