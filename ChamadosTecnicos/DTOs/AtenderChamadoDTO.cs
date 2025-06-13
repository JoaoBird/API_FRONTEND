using System.ComponentModel.DataAnnotations;

namespace ChamadosTecnicos.DTOs
{
    public class AtenderChamadoDTO
    {
        [Required(ErrorMessage = "TecnicoId é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "TecnicoId deve ser maior que 0")]
        public int TecnicoId { get; set; }
    }
}