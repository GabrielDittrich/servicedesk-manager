using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Api.DTOs.Usuarios
{
    public class CriarUsuarioRequest
    {
        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public PerfilUsuario Perfil { get; set; } = PerfilUsuario.Solicitante;
    }
}