using GerenciamentoTarefas.Domain.Core.Interfaces.Repositories;
using GerenciamentoTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Domain.Services
{
    public class ComentarioService : BaseService<Comentario>
    {
        private readonly IBaseRepository<Comentario> _comentarioRepository;
        public ComentarioService(IBaseRepository<Comentario> comentarioRepository) : base(comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }
    }
}
