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

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Adicionar(TEntity obj)
        {
            try
            {
                _repository.Adicionar(obj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao adicionar o item.", ex);
            }
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            try
            {
                return _repository.ObterPorId(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao obter o item com ID {id}.", ex);
            }
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            try
            {
                return _repository.ObterTodos();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter os itens.", ex);
            }
        }

        public virtual void Atualizar(TEntity obj)
        {
            try
            {
                _repository.Atualizar(obj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao atualizar o item.", ex);
            }
        }

        public virtual void Remover(TEntity obj)
        {
            try
            {
                _repository.Remover(obj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao remover o item.", ex);
            }
        }

        public virtual void Dispose()
        {
            _repository.Dispose();
        }
    }

}
