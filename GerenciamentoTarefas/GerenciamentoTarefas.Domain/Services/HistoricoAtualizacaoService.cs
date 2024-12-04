using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Services
{
    public class HistoricoAtualizacaoService : BaseService<HistoricoAtualizacao>
    {
        private readonly IBaseRepository<HistoricoAtualizacao> _historicoAtualizacaoRepository;
        public HistoricoAtualizacaoService(IBaseRepository<HistoricoAtualizacao> historicoAtualizacaoRepository) : base(historicoAtualizacaoRepository)
        {
            _historicoAtualizacaoRepository = historicoAtualizacaoRepository;
        }
    }
}
