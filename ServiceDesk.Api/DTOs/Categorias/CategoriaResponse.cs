namespace ServiceDesk.Api.DTOs.Categorias
{
    public class CategoriaResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public bool Ativa { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}