using ChamadosTecnicos.Models;

namespace ChamadosTecnicos.Data
{
    public static class DataStore
    {
        // Listas compartilhadas entre todas as rotas
        public static List<Chamado> Chamados { get; set; } = new();
        public static List<Usuario> Usuarios { get; set; } = new();
        public static List<Tecnico> Tecnicos { get; set; } = new();
        public static List<Problema> Problemas { get; set; } = new();
        public static List<Departamento> Departamentos { get; set; } = new();

        public static void InicializarDados()
        {
            // Limpa dados existentes
            Chamados.Clear();
            Usuarios.Clear();
            Tecnicos.Clear();
            Problemas.Clear();
            Departamentos.Clear();

            // Inicializa departamentos
            Departamentos.AddRange(new[]
            {
                new Departamento { Id = 1, Nome = "TI" },
                new Departamento { Id = 2, Nome = "Recursos Humanos" },
                new Departamento { Id = 3, Nome = "Financeiro" }
            });

            // Inicializa problemas
            Problemas.AddRange(new[]
            {
                new Problema { Id = 1, Nome = "Hardware", Descricao = "Problemas relacionados a hardware", SolucaoComum = "Verificar conexões e componentes", DepartamentoId = 1 },
                new Problema { Id = 2, Nome = "Software", Descricao = "Problemas relacionados a software", SolucaoComum = "Reinstalar ou atualizar software", DepartamentoId = 1 },
                new Problema { Id = 3, Nome = "Rede", Descricao = "Problemas de conectividade", SolucaoComum = "Verificar cabos e configurações de rede", DepartamentoId = 1 }
            });

            // Inicializa usuários
            Usuarios.AddRange(new[]
            {
                new Usuario { Id = 1, Nome = "João Silva", Email = "joao@empresa.com", Senha = "123456" },
                new Usuario { Id = 2, Nome = "Maria Santos", Email = "maria@empresa.com", Senha = "123456" },
                new Usuario { Id = 3, Nome = "Pedro Oliveira", Email = "pedro@empresa.com", Senha = "123456" }
            });

            // Inicializa técnicos
            Tecnicos.AddRange(new[]
            {
                new Tecnico { Id = 1, Nome = "Carlos Tech", Especialidade = "Hardware" },
                new Tecnico { Id = 2, Nome = "Ana Support", Especialidade = "Software" },
                new Tecnico { Id = 3, Nome = "Roberto Fix", Especialidade = "Redes" }
            });

            // Inicializa chamados
            Chamados.AddRange(new[]
            {
                new Chamado 
                { 
                    Id = 1, 
                    UsuarioId = 1, 
                    ProblemaId = 1,
                    Titulo = "Computador não liga", 
                    Descricao = "O computador não está ligando após queda de energia",
                    Status = StatusChamado.Aberto,
                    DataAbertura = DateTime.Now.AddDays(-2),
                    Urgente = true,
                    Remoto = false
                },
                new Chamado 
                { 
                    Id = 2, 
                    UsuarioId = 2, 
                    ProblemaId = 2,
                    Titulo = "Problema na impressora", 
                    Descricao = "Impressora não está imprimindo corretamente",
                    Status = StatusChamado.EmAndamento,
                    TecnicoId = 1,
                    DataAbertura = DateTime.Now.AddDays(-1),
                    Urgente = false,
                    Remoto = true
                },
                new Chamado 
                { 
                    Id = 3, 
                    UsuarioId = 3, 
                    ProblemaId = 3,
                    Titulo = "Internet lenta", 
                    Descricao = "Conexão com internet muito lenta",
                    Status = StatusChamado.Aberto,
                    DataAbertura = DateTime.Now,
                    Urgente = false,
                    Remoto = true
                }
            });

            Console.WriteLine($"✅ Dados inicializados: {Chamados.Count} chamados, {Usuarios.Count} usuários, {Tecnicos.Count} técnicos");
        }
    }
}