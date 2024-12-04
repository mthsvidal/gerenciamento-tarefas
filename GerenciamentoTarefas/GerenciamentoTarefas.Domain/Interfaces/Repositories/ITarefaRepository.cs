using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Interfaces.Repositories
{
    public interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        void Atualizar(Tarefa objeto, Guid usuarioId);
    }
}
