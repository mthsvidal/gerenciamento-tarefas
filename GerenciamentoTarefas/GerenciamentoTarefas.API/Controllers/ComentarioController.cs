using AutoMapper;
using GerenciamentoTarefas.Domain.DTOs;
using GerenciamentoTarefas.Domain.Models;
using GerenciamentoTarefas.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoTarefas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly ComentarioService _comentarioService;
        private readonly ILogger<ComentarioController> _logger;
        private readonly IMapper _mapper;

        public ComentarioController(ComentarioService comentarioService, ILogger<ComentarioController> logger, IMapper mapper)
        {
            _comentarioService = comentarioService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um comentário a uma tarefa.
        /// </summary>
        /// <param name="objeto">Objeto contendo as informações do comentário a ser adicionado a uma tarefa.</param>
        /// <returns>Retorna o ID do comentário adicionado.</returns>
        [HttpPost]
        public ActionResult Adicionar([FromBody] ComentarioInputDto objeto)
        {
            if (objeto == null)
            {
                return BadRequest("A tarefa não pode ser nula");
            }

            try
            {
                var comentario = _mapper.Map<Comentario>(objeto);

                _comentarioService.Adicionar(comentario);

                return Ok(comentario.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar comentário a uma tarefa");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
