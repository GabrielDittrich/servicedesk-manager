namespace ServiceDesk.Api.DTOs.Atendimentos
{
    public class AtendimentoResponse
    {
        public int Id { get; set; }

        public int ChamadoId { get; set; }

        public int TecnicoId { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public DateTime CriadoEm { get; set; }
    }
}