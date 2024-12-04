using AutoMapper;
using GerenciamentoTarefas.Domain.DTOs;
using GerenciamentoTarefas.Domain.Models;
using GerenciamentoTarefas.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciamentoTarefas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly ProjetoService _projetoService;
        private readonly ILogger<ProjetoController> _logger;
        private readonly IMapper _mapper;

        public ProjetoController(ProjetoService projetoService, ILogger<ProjetoController> logger, IMapper mapper)
        {
            _projetoService = projetoService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém a lista de projetos associados a um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário para filtrar os projetos.</param>
        /// <returns>Lista de projetos do usuário.</returns>
        [HttpGet("usuario/{usuarioId}")]
        public ActionResult<IEnumerable<Projeto>> ListarPorUsuarioId(Guid usuarioId)
        {
            try
            {
                var projetos = _projetoService.ObterPorUsuarioId(usuarioId);

                if (projetos == null || !projetos.Any())
                    return NotFound($"Nenhum projeto encontrado para o usuário com ID: {usuarioId}");

                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter projetos para o usuário {usuarioId}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo projeto.
        /// </summary>
        /// <param name="objeto">Objeto contendo as informações do projeto a ser adicionado.</param>
        /// <returns>Retorna o ID do projeto adicionado.</returns>
        [HttpPost]
        public ActionResult Adicionar([FromBody] ProjetoInputDto objeto)
        {
            if (objeto == null)
            {
                return BadRequest("O projeto não pode ser nulo");
            }

            try
            {
                var projeto = _mapper.Map<Projeto>(objeto);

                _projetoService.Adicionar(projeto);

                return Ok(projeto.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar projeto");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Remove um projeto pelo ID.
        /// </summary>
        /// <param name="id">ID do projeto a ser removido.</param>
        /// <returns>Retorna um código de status 204 (No Content) se o projeto for removido com sucesso.</returns>
        [HttpDelete("{id}")]
        public ActionResult Remover(Guid id)
        {
            try
            {
                var projeto = _projetoService.ObterPorId(id);

                if (projeto == null)
                    return NotFound($"Projeto com ID {id} não encontrado");                

                _projetoService.Remover(projeto);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover projeto com ID {id}");

                return StatusCode(500, ex.Message);
            }
        }
    }
}
