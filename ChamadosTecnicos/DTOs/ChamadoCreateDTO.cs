using System.ComponentModel.DataAnnotations;

namespace ChamadosTecnicos.DTOs;

public class ChamadoCreateDTO
{
    [Required(ErrorMessage = "Título é obrigatório")]
    [StringLength(200, ErrorMessage = "Título deve ter no máximo 200 caracteres")]
    public string Titulo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
    public string Descricao { get; set; } = string.Empty;
    
    public bool Urgente { get; set; }
    public bool Remoto { get; set; }
    
    [Required(ErrorMessage = "UsuarioId é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "UsuarioId deve ser maior que 0")]
    public int UsuarioId { get; set; }
    
    [Required(ErrorMessage = "ProblemaId é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "ProblemaId deve ser maior que 0")]
    public int ProblemaId { get; set; }
}