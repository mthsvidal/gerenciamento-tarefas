using GerenciamentoTarefas.Domain.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.DTOs
{
    public class TarefaUpdateDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Detalhes { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataVencimento { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        public PrioridadeTarefa Prioridade { get; set; }
    }
}
