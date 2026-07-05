using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Api.DTOs.Chamados;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Domain.Enums;
using ServiceDesk.Business.Interfaces;

namespace ServiceDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadosController : ControllerBase
    {
        private readonly IChamadoService _chamadoService;

        public ChamadosController(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var chamados = _chamadoService.Listar();

            return Ok(chamados);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var chamado = _chamadoService.BuscarPorId(id);

            if (chamado == null)
            {
                return NotFound("Chamado não encontrado.");
            }

            return Ok(chamado);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] CriarChamadoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Titulo))
            {
                return BadRequest("O título do chamado é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.Descricao))
            {
                return BadRequest("A descrição do chamado é obrigatória.");
            }

            if (request.CategoriaId <= 0)
            {
                return BadRequest("A categoria do chamado é obrigatória.");
            }

            if (request.SolicitanteId <= 0)
            {
                return BadRequest("O solicitante do chamado é obrigatório.");
            }

            var chamado = new Chamado
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                CategoriaId = request.CategoriaId,
                SolicitanteId = request.SolicitanteId,
                Prioridade = request.Prioridade
            };

            _chamadoService.Criar(chamado);

            return CreatedAtAction(nameof(BuscarPorId), new { id = chamado.Id }, chamado);
        }

        [HttpPatch("{id}/status")]
        public IActionResult AlterarStatus(int id, [FromBody] AlterarStatusChamadoRequest request)
        {
            var chamado = _chamadoService.BuscarPorId(id);

            if (chamado == null)
            {
                return NotFound("Chamado não encontrado.");
            }

            _chamadoService.AlterarStatus(id, request.Status, request.TecnicoResponsavelId);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Cancelar(int id)
        {
            var chamado = _chamadoService.BuscarPorId(id);

            if (chamado == null)
            {
                return NotFound("Chamado não encontrado.");
            }

            _chamadoService.AlterarStatus(id, StatusChamado.Cancelado, chamado.TecnicoResponsavelId);

            return NoContent();
        }
    }
}