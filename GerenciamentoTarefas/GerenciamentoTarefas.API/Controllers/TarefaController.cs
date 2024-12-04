using AutoMapper;
using GerenciamentoTarefas.Domain.DTOs;
using GerenciamentoTarefas.Domain.Models;
using GerenciamentoTarefas.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoTarefas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {        
        private readonly TarefaService _tarefaService;
        private readonly ILogger<TarefaController> _logger;
        private readonly IMapper _mapper;

        public TarefaController(TarefaService tarefaService, ILogger<TarefaController> logger, IMapper mapper)
        {
            _tarefaService = tarefaService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém a lista de tarefas associados a um projeto específico.
        /// </summary>
        /// <param name="projetoId">ID do projeto para filtrar as tarefas.</param>
        /// <returns>Lista de tarefas de um projeto.</returns>
        [HttpGet("projeto/{projetoId}")]
        public ActionResult<IEnumerable<Tarefa>> ListarPorProjetoId(Guid projetoId)
        {
            try
            {
                var tarefas = _tarefaService.ObterPorProjetoId(projetoId);

                if (tarefas == null || !tarefas.Any())
                    return NotFound($"Nenhuma tarefa encontrada para o projeto com ID: {projetoId}");

                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter tarefas para o projeto {projetoId}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Adiciona uma nova tarefa.
        /// </summary>
        /// <param name="objeto">Objeto contendo as informações da tarefa a ser adicionada.</param>
        /// <returns>Retorna o ID da tarefa adicionada.</returns>
        [HttpPost]
        public ActionResult Adicionar([FromBody] TarefaInputDto objeto)
        {
            if (objeto == null)
            {
                return BadRequest("A tarefa não pode ser nula");
            }

            try
            {
                var tarefa = _mapper.Map<Tarefa>(objeto);

                _tarefaService.Adicionar(tarefa);

                return Ok(tarefa.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar tarefa");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Remove uma tarefa pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa a ser removida.</param>
        /// <returns>Retorna um código de status 204 (No Content) se a tarefa for removida com sucesso.</returns>
        [HttpDelete("{id}")]
        public ActionResult Remover(Guid id)
        {
            try
            {
                var tarefa = _tarefaService.ObterPorId(id);

                if (tarefa == null)
                    return NotFound($"Tarefa com ID {id} não encontrada");

                _tarefaService.Remover(tarefa);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover tarefa com ID {id}");

                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="objeto">Objeto contendo as informações atualizadas da tarefa.</param>
        /// <returns>Retorna o status 200 (OK) se a tarefa for atualizada com sucesso, ou mensagens de erro.</returns>
        [HttpPut]
        public ActionResult Atualizar([FromBody] TarefaUpdateDto objeto)
        {
            if (objeto == null)
            {
                return BadRequest("A tarefa não pode ser nula.");
            }

            try
            {
                // Mapeia as alterações para a tarefa existente
                var tarefaAtualizada = _mapper.Map<Tarefa>(objeto);

                // Atualiza a tarefa
                _tarefaService.Atualizar(tarefaAtualizada, objeto.UsuarioId);                

                return Ok("Tarefa atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar a tarefa com ID {objeto.Id}");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
