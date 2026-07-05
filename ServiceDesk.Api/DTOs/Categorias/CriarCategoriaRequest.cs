namespace ServiceDesk.Api.DTOs.Categorias
{
    public class CriarCategoriaRequest
    {
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }
    }
}