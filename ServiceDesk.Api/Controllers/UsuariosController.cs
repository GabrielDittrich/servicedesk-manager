using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Api.DTOs.Usuarios;
using ServiceDesk.Business.Interfaces;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _usuarioService.Listar();

            var response = usuarios.Select(MapearParaResponse).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var usuario = _usuarioService.BuscarPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var response = MapearParaResponse(usuario);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] CriarUsuarioRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                return BadRequest("O nome do usuário é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("O e-mail do usuário é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest("A senha do usuário é obrigatória.");
            }

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Perfil = request.Perfil
            };

            _usuarioService.Criar(usuario, request.Senha);

            var response = MapearParaResponse(usuario);

            return CreatedAtAction(nameof(BuscarPorId), new { id = usuario.Id }, response);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var usuario = _usuarioService.BuscarPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _usuarioService.Remover(usuario);

            return NoContent();
        }

        private static UsuarioResponse MapearParaResponse(Usuario usuario)
        {
            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Ativo = usuario.Ativo,
                CriadoEm = usuario.CriadoEm
            };
        }
    }
}