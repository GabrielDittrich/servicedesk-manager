using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Api.DTOs.Auth
{
    public class LoginResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public PerfilUsuario Perfil { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}