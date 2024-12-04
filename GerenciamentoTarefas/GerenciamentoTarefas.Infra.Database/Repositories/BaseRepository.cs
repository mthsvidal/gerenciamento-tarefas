using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GerenciamentoTarefas.Infra.Database.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly GerenciamentoTarefasContext _context;

        // O DbContext é passado para o repositório via injeção de dependência
        public BaseRepository(GerenciamentoTarefasContext context)
        {
            _context = context;
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (_context.ChangeTracker.Entries().FirstOrDefault((EntityEntry ent) => ent.Entity == entity) == null)
            {
                _context.Attach(entity);
            }
        }

        // Adiciona um objeto no banco
        public void Adicionar(TEntity obj)
        {
            try
            {
                _context.Set<TEntity>().Add(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao adicionar o objeto", ex);
            }
        }

        // Retorna um objeto pelo ID
        public TEntity ObterPorId(Guid id)
        {
            try
            {
                // Verifica se a entidade tem relacionamentos configurados
                var query = _context.Set<TEntity>().AsNoTracking();

                // Inclui todas as propriedades de navegação da entidade
                var navigationProperties = _context.Model.FindEntityType(typeof(TEntity))?
                    .GetNavigations()
                    .Select(n => n.Name);

                if (navigationProperties != null)
                {
                    foreach (var property in navigationProperties)
                    {
                        query = query.Include(property);
                    }
                }

                // Busca o objeto pelo ID
                return query.SingleOrDefault(e => EF.Property<Guid>(e, "Id") == id);
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao obter o objeto pelo ID", ex);
            }
        }

        // Retorna um objeto pelo ID
        public TEntity ObterEntidadePorId(Guid id)
        {
            try
            {
                // Busca a entidade sem relacionamentos, usando AsNoTracking para melhorar a performance
                return _context.Set<TEntity>()
                               .AsNoTracking()  // Não faz o tracking das alterações
                               .SingleOrDefault(e => EF.Property<Guid>(e, "Id") == id);
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao obter o objeto pelo ID", ex);
            }
        }

        // Retorna todos os objetos de uma entidade
        public IEnumerable<TEntity> ObterTodos()
        {
            try
            {
                // Obtém a consulta inicial
                var query = _context.Set<TEntity>().AsQueryable();

                // Inclui todas as propriedades de navegação da entidade
                var navigationProperties = _context.Model.FindEntityType(typeof(TEntity))?
                    .GetNavigations()
                    .Select(n => n.Name);

                if (navigationProperties != null)
                {
                    foreach (var property in navigationProperties)
                    {
                        query = query.Include(property);
                    }
                }

                // Retorna todos os registros
                return query.ToList();
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao obter todos os objetos", ex);
            }
        }


        // Retorna todos a partir de um filtro
        public IEnumerable<TEntity> ObterComFiltro(Func<TEntity, bool> predicado)
        {
            try
            {
                // Obtém a consulta inicial
                var query = _context.Set<TEntity>().AsNoTracking();

                // Inclui todas as propriedades de navegação da entidade
                var navigationProperties = _context.Model.FindEntityType(typeof(TEntity))?
                    .GetNavigations()
                    .Select(n => n.Name);

                if (navigationProperties != null)
                {
                    foreach (var property in navigationProperties)
                    {
                        query = query.Include(property);
                    }
                }

                // Aplica o filtro e retorna os registros
                return query.Where(predicado).ToList();
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao buscar com filtro", ex);
            }
        }


        // Atualiza um objeto no banco
        public void Atualizar(TEntity obj)
        {
            try
            {
                AttachIfNot(obj);
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao atualizar o objeto", ex);
            }
        }



        // Remove um objeto do banco
        public void Remover(TEntity obj)
        {
            try
            {
                _context.Set<TEntity>().Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao remover o objeto", ex);
            }
        }

        // Libera os recursos
        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao liberar os recursos", ex);
            }
        }
    }

}
