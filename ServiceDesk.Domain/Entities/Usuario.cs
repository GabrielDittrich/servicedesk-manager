using ServiceDesk.Domain.Enums;

namespace ServiceDesk.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;

        public PerfilUsuario Perfil { get; set; } = PerfilUsuario.Solicitante;

        public bool Ativo { get; set; } = true;

        public DateTime CriadoEm { get; set; } = DateTime.Now;

    }
}