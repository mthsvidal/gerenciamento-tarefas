using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Enumerables;
using GerenciamentoTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Services
{
    public class ProjetoService : BaseService<Projeto>
    {
        private readonly IBaseRepository<Projeto> _projetoRepository;

        public ProjetoService(IBaseRepository<Projeto> projetoRepository) : base(projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public IEnumerable<Projeto> ObterPorUsuarioId(Guid usuarioId)
        {
            try
            {
                return _projetoRepository.ObterComFiltro(p => p.UsuarioId == usuarioId);                                         
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao obter projetos para o usuário com ID {usuarioId}.", ex);
            }
        }

        public override void Remover(Projeto objeto)
        {
            try
            {
                var projeto = _projetoRepository.ObterPorId(objeto.Id);

                if (projeto.Tarefas.Where(x => x.Status == StatusTarefa.Pendente).Count() > 0)
                    throw new Exception("Não foi possível remover o projeto. Para continuar, por favor, finalize ou remova as tarefas pendentes associadas.");

                _projetoRepository.Remover(objeto);
            }
            catch (Exception ex)
            {
                // Loga o erro ou lança uma exceção personalizada
                throw new InvalidOperationException($"Erro ao remover o projeto com ID {objeto.Id}. Motivo: {ex.Message}", ex);
            }
        }
    }
}
