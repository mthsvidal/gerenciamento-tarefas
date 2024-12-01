using GerenciamentoTarefas.Domain.Models;
using GerenciamentoTarefas.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoTarefas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly ProjetoService _projetoService;
        private readonly ILogger<ProjetoController> _logger;

        public ProjetoController(ProjetoService projetoService, ILogger<ProjetoController> logger)
        {
            _projetoService = projetoService;
            _logger = logger;
        }

        // Endpoint para obter todos os projetos
        [HttpGet]
        public ActionResult<IEnumerable<Projeto>> ObterTodos()
        {
            try
            {
                var projetos = _projetoService.ObterTodos();
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os projetos");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Endpoint para obter projetos por usuário
        [HttpGet("usuario/{usuarioId}")]
        public ActionResult<IEnumerable<Projeto>> ObterPorUsuarioId(string usuarioId)
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
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Endpoint para buscar projetos por nome
        [HttpGet("nome/{nome}")]
        public ActionResult<IEnumerable<Projeto>> ObterPorNome(string nome)
        {
            try
            {
                var projetos = _projetoService.ObterPorNome(nome);
                if (projetos == null || !projetos.Any())
                    return NotFound($"Nenhum projeto encontrado com o nome: {nome}");
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter projetos com nome {nome}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Endpoint para verificar se o projeto tem tarefas associadas
        [HttpGet("{projetoId}/tem-tarefas")]
        public ActionResult<bool> TemTarefasAssociadas(Guid projetoId)
        {
            try
            {
                var temTarefas = _projetoService.TemTarefasAssociadas(projetoId);
                return Ok(temTarefas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao verificar tarefas associadas ao projeto {projetoId}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Endpoint para adicionar um novo projeto
        [HttpPost]
        public ActionResult Adicionar([FromBody] Projeto projeto)
        {
            if (projeto == null)
            {
                return BadRequest("O projeto não pode ser nulo");
            }

            try
            {
                _projetoService.Adicionar(projeto);
                return CreatedAtAction(nameof(ObterPorId), new { id = projeto.Id }, projeto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar projeto");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Endpoint para obter um projeto por ID
        [HttpGet("{id}")]
        public ActionResult<Projeto> ObterPorId(Guid id)
        {
            try
            {
                var projeto = _projetoService.ObterPorId(id);
                if (projeto == null)
                    return NotFound($"Projeto com ID {id} não encontrado");
                return Ok(projeto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter projeto com ID {id}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Endpoint para atualizar um projeto
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, [FromBody] Projeto projeto)
        {
            if (projeto == null || id != projeto.Id)
            {
                return BadRequest("Dados inválidos para atualização");
            }

            try
            {
                _projetoService.Atualizar(projeto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar projeto com ID {id}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // Endpoint para remover um projeto
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
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
