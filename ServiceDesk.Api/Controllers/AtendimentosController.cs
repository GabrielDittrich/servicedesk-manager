using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Api.DTOs.Atendimentos;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Api.Controllers
{
    [Route("api/chamados/{chamadoId}/atendimentos")]
    [ApiController]
    public class AtendimentosController : ControllerBase
    {
        private readonly IAtendimentoService _atendimentoService;

        public AtendimentosController(IAtendimentoService atendimentoService)
        {
            _atendimentoService = atendimentoService;
        }

        [HttpGet]
        public IActionResult ListarPorChamado(int chamadoId)
        {
            var atendimentos = _atendimentoService.ListarPorChamado(chamadoId);

            return Ok(atendimentos);
        }

        [HttpPost]
        public IActionResult Criar(int chamadoId, [FromBody] CriarAtendimentoRequest request)
        {
            if (request.TecnicoId <= 0)
            {
                return BadRequest("O técnico responsável é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.Descricao))
            {
                return BadRequest("A descrição do atendimento é obrigatória.");
            }

            var atendimento = new Atendimento
            {
                ChamadoId = chamadoId,
                TecnicoId = request.TecnicoId,
                Descricao = request.Descricao
            };

            _atendimentoService.Criar(atendimento);

            return Created(string.Empty, atendimento);
        }
    }
}