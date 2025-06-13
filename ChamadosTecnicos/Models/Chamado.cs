using System;
using System.ComponentModel.DataAnnotations;

namespace ChamadosTecnicos.Models
{
    public class Chamado
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Titulo { get; set; } = string.Empty;
        
        [Required]
        public string Descricao { get; set; } = string.Empty;
        
        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataFechamento { get; set; }
        
        [Required]
        public bool Urgente { get; set; }
        
        [Required]
        public bool Remoto { get; set; }
        
        public string NotaAtendimento { get; set; } = string.Empty;
        public string TempoGasto { get; set; } = string.Empty;
        
        [Required]
        public StatusChamado Status { get; set; } = StatusChamado.Aberto;
        
        // Relacionamentos
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        
        public int? TecnicoId { get; set; }
        public Tecnico? Tecnico { get; set; }
        
        public int ProblemaId { get; set; }
        public Problema Problema { get; set; } = null!;
    }

    public enum StatusChamado
    {
        Aberto,
        EmAndamento,
        Resolvido,
        Fechado
    }
}