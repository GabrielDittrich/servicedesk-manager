namespace ServiceDesk.Api.DTOs.Atendimentos
{
    public class CriarAtendimentoRequest
    {
        public int TecnicoId { get; set; }

        public string Descricao { get; set; } = string.Empty;
    }
}