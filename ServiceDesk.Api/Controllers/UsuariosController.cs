using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Api.DTOs.Usuarios;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Business.Interfaces;

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

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var usuario = _usuarioService.BuscarPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
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

            return CreatedAtAction(nameof(BuscarPorId), new { id = usuario.Id }, usuario);
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
    }
}