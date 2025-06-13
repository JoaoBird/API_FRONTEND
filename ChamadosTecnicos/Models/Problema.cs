using System.Collections.Generic;

namespace ChamadosTecnicos.Models
{
    public class Problema
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string SolucaoComum { get; set; } = string.Empty;
        
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;
        
    }
}