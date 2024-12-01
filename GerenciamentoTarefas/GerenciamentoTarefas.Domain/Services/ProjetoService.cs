using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
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

        public IEnumerable<Projeto> ObterPorUsuarioId(string usuarioId)
        {
            return _projetoRepository.ObterTodos()
                                     .Where(p => p.UsuarioId == usuarioId);
        }

        public IEnumerable<Projeto> ObterPorNome(string nome)
        {
            return _projetoRepository.ObterTodos()
                                     .Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
        }

        public bool TemTarefasAssociadas(Guid projetoId)
        {
            var projeto = _projetoRepository.ObterPorId(projetoId);
            return projeto?.Tarefas.Any() ?? false;
        }
    }
}
