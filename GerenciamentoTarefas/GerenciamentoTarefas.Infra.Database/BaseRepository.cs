using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;

namespace GerenciamentoTarefas.Infra.Database
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly GerenciamentoTarefasContext _context;

        // O DbContext é passado para o repositório via injeção de dependência
        public BaseRepository(GerenciamentoTarefasContext context)
        {
            _context = context;
        }

        // Adiciona um objeto no banco
        public void Adicionar(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        // Retorna um objeto pelo ID
        public TEntity ObterPorId(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        // Retorna todos os objetos de uma entidade
        public IEnumerable<TEntity> ObterTodos()
        {
            return _context.Set<TEntity>().ToList();
        }

        // Atualiza um objeto no banco
        public void Atualizar(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            _context.SaveChanges();
        }

        // Remove um objeto do banco
        public void Remover(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        // Libera os recursos
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
