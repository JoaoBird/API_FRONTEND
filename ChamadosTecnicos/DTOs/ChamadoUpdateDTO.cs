using System.ComponentModel.DataAnnotations;

namespace ChamadosTecnicos.DTOs
{
    public class ChamadoUpdateDTO
    {
        [Required(ErrorMessage = "NotaAtendimento é obrigatória")]
        [StringLength(1000, ErrorMessage = "NotaAtendimento deve ter no máximo 1000 caracteres")]
        public string NotaAtendimento { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "TempoGasto é obrigatório")]
        [StringLength(50, ErrorMessage = "TempoGasto deve ter no máximo 50 caracteres")]
        public string TempoGasto { get; set; } = string.Empty;
    }
}