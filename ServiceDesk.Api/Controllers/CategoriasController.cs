using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Api.DTOs.Categorias;
using ServiceDesk.Api.DTOs.Categorias;

namespace ServiceDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var categorias = _categoriaService.Listar();

            var response = categorias.Select(MapearParaResponse).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var categoria = _categoriaService.BuscarPorId(id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            var response = MapearParaResponse(categoria);

            return Ok(response);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Criar([FromBody] CriarCategoriaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                return BadRequest("O nome da categoria é obrigatório.");
            }

            var categoria = new Categoria
            {
                Nome = request.Nome,
                Descricao = request.Descricao
            };

            _categoriaService.Criar(categoria);

            var response = MapearParaResponse(categoria);

            return CreatedAtAction(nameof(BuscarPorId), new { id = categoria.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Categoria categoria)
        {
            var categoriaExistente = _categoriaService.BuscarPorId(id);

            if (categoriaExistente == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            categoria.Id = id;

            _categoriaService.Atualizar(categoria);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var categoria = _categoriaService.BuscarPorId(id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            _categoriaService.Remover(categoria);

            return NoContent();
        }
        private static CategoriaResponse MapearParaResponse(Categoria categoria)
        {
            return new CategoriaResponse
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativa = categoria.Ativa,
                CriadoEm = categoria.CriadoEm
            };
        }

    }

}
