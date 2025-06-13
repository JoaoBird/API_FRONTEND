using System;

namespace ChamadosTecnicos.DTOs
{
    public class ChamadoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public bool Urgente { get; set; }
        public bool Remoto { get; set; }
        public string Status { get; set; } = string.Empty;
        public string NotaAtendimento { get; set; } = string.Empty;
        public string TempoGasto { get; set; } = string.Empty;
        public string UsuarioNome { get; set; } = string.Empty;
        public string? TecnicoNome { get; set; }
        public string ProblemaNome { get; set; } = string.Empty;
        public string DepartamentoNome { get; set; } = string.Empty;
    }
}