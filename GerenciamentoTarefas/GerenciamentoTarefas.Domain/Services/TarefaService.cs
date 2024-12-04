using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.DTOs;
using GerenciamentoTarefas.Domain.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Models;

namespace GerenciamentoTarefas.Domain.Services
{
    public class TarefaService : BaseService<Tarefa>
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository) : base(tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public override void Adicionar(Tarefa objeto)
        {
            var tarefasProjeto = _tarefaRepository.ObterComFiltro(x => x.ProjetoId == objeto.ProjetoId);

            if (tarefasProjeto.Count() >= 20)
            {
                // Se o limite for atingido, lança uma exceção com uma mensagem personalizada
                throw new InvalidOperationException("Cada projeto tem um limite máximo de 20 tarefas. Não é possível adicionar mais tarefas.");
            }

            _tarefaRepository.Adicionar(objeto);
        }

        public IEnumerable<Tarefa> ObterPorProjetoId(Guid projetoId)
        {
            try
            {
                return _tarefaRepository.ObterComFiltro(p => p.ProjetoId == projetoId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao obter tarefas para o projeto com ID {projetoId}.", ex);
            }
        }

        public void Atualizar(Tarefa objeto, Guid usuarioId)
        {
            try
            {
                var tarefaExistente = _tarefaRepository.ObterEntidadePorId(objeto.Id);

                if (tarefaExistente == null)
                {
                    throw new InvalidOperationException("Tarefa não encontrada.");
                }

                // Verifica se a prioridade foi alterada
                if (tarefaExistente.Prioridade != objeto.Prioridade)
                {
                    throw new InvalidOperationException("Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");
                }

                objeto.ProjetoId = tarefaExistente.ProjetoId;

                // Atualiza a tarefa
                _tarefaRepository.Atualizar(objeto, usuarioId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar o item. Motivo: {ex.Message}", ex);
            }
        }
    }
}
