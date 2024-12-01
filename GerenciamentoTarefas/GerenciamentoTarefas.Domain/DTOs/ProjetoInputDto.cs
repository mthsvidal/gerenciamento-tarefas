using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.DTOs
{
    public class ProjetoInputDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UsuarioId { get; set; }
    }
}
