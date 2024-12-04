using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Infra.Database.Repositories
{
    public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        protected readonly GerenciamentoTarefasContext _context;

        public TarefaRepository(GerenciamentoTarefasContext context) :base(context) 
        {
            _context = context;
        }

        public void Atualizar(Tarefa objeto, Guid usuarioId)
        {
            try
            {
                AttachIfNot(objeto);

                _context.Entry(objeto).State = EntityState.Modified;

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                // Logar ou tratar o erro de acordo com as necessidades
                throw new InvalidOperationException("Erro ao atualizar o objeto", ex);
            }
        }
    }
}
