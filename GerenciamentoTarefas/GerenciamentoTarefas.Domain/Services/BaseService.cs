using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Services
{
    public abstract class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> Repository)
        {
            _repository = Repository;
        }
        public virtual void Adicionar(TEntity obj)
        {
            _repository.Adicionar(obj);
        }
        public virtual TEntity ObterPorId(Guid id)
        {
            return _repository.ObterPorId(id);
        }
        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return _repository.ObterTodos();
        }
        public virtual void Atualizar(TEntity obj)
        {
            _repository.Atualizar(obj);
        }
        public virtual void Remover(TEntity obj)
        {
            _repository.Remover(obj);
        }

        public virtual void Dispose()
        {
            _repository.Dispose();
        }
    }
}
