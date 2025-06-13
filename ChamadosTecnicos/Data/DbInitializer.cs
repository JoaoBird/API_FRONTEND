using System;
using System.Linq;
using ChamadosTecnicos.Models;

namespace ChamadosTecnicos.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Usuarios.Any())
            {
                return; // DB já foi inicializado
            }

            // Departamentos
            var departamentos = new Departamento[]
            {
                new Departamento{Nome="TI"},
                new Departamento{Nome="RH"},
                new Departamento{Nome="Financeiro"}
            };
            
            context.Departamentos.AddRange(departamentos);
            context.SaveChanges();

            // Problemas
            var problemas = new Problema[]
            {
                 new Problema{Nome="Email inacessível", Descricao="Erro ao acessar email", SolucaoComum="Reiniciar aplicação", DepartamentoId=1},
                new Problema{Nome="Erro de login", Descricao="Senha não funciona", SolucaoComum="Redefinir senha", DepartamentoId=1},
            new Problema{Nome="Problema de rede", Descricao="Sem conexão", SolucaoComum="Verificar roteador", DepartamentoId=1},
            new Problema{Nome="Sistema RH falha", Descricao="Erro ao abrir sistema", SolucaoComum="Reinstalar sistema", DepartamentoId=2},
            new Problema{Nome="Folha duplicada", Descricao="Folha paga duas vezes", SolucaoComum="Verificar lançamentos", DepartamentoId=2},
                new Problema{Nome="Erro na folha", Descricao="Dados inconsistentes", SolucaoComum="Validar cadastro", DepartamentoId=2},
                 new Problema{Nome="Erro de orçamento", Descricao="Planilha corrompida", SolucaoComum="Restaurar backup", DepartamentoId=3},
        new Problema{Nome="Pagamento não identificado", Descricao="Transação sumiu", SolucaoComum="Revisar extrato", DepartamentoId=3},
        new Problema{Nome="Erro em notas fiscais", Descricao="Cálculo errado", SolucaoComum="Corrigir fórmula", DepartamentoId=3},
        new Problema{Nome="Sistema Financeiro lento", Descricao="Demora no carregamento", SolucaoComum="Aumentar memória", DepartamentoId=3}
            };
            
            context.Problemas.AddRange(problemas);
            context.SaveChanges();

            // Usuários
            var usuarios = new Usuario[]
            {
            new Usuario{Nome="João Silva", Email="joao@empresa.com", Senha="123456"},
            new Usuario{Nome="Maria Souza", Email="maria@empresa.com", Senha="123456"},
            new Usuario{Nome="Carlos Oliveira", Email="carlos@empresa.com", Senha="123456"},
            new Usuario{Nome="Ana Lima", Email="ana@empresa.com", Senha="123456"},
            new Usuario{Nome="Paulo Mendes", Email="paulo@empresa.com", Senha="123456"},
            new Usuario{Nome="Fernanda Costa", Email="fernanda@empresa.com", Senha="123456"},
            new Usuario{Nome="Rafael Almeida", Email="rafael@empresa.com", Senha="123456"},
            new Usuario{Nome="Juliana Martins", Email="juliana@empresa.com", Senha="123456"},
            new Usuario{Nome="Bruno Rocha", Email="bruno@empresa.com", Senha="123456"},
            new Usuario{Nome="Laura Ferreira", Email="laura@empresa.com", Senha="123456"}
            };
            
            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            // Técnicos
            var tecnicos = new Tecnico[]
            {
            new Tecnico{Nome="Técnico A", Especialidade="Redes"},
            new Tecnico{Nome="Técnico B", Especialidade="Hardware"},
            new Tecnico{Nome="Técnico C", Especialidade="Software"},
            new Tecnico{Nome="Técnico D", Especialidade="RH"},
            new Tecnico{Nome="Técnico E", Especialidade="Financeiro"},
            new Tecnico{Nome="Técnico F", Especialidade="Servidores"},
            new Tecnico{Nome="Técnico G", Especialidade="Sistemas Web"},
            new Tecnico{Nome="Técnico H", Especialidade="Infraestrutura"},
            new Tecnico{Nome="Técnico I", Especialidade="Datacenter"},
            new Tecnico{Nome="Técnico J", Especialidade="Backup e Recuperação"}
            };
            
            context.Tecnicos.AddRange(tecnicos);
            context.SaveChanges();

            // Chamados
            var chamados = new Chamado[]
            {
        new Chamado{Titulo="Email não funciona", Descricao="Não consigo enviar emails", Urgente=true, Remoto=true, UsuarioId=1, ProblemaId=1},
        new Chamado{Titulo="Erro de login", Descricao="Senha incorreta", Urgente=false, Remoto=false, UsuarioId=2, ProblemaId=2},
        new Chamado{Titulo="Rede caiu", Descricao="Sem internet no setor", Urgente=true, Remoto=false, UsuarioId=3, ProblemaId=3, TecnicoId=1, Status=StatusChamado.EmAndamento},
        new Chamado{Titulo="RH com falha", Descricao="Sistema fecha sozinho", Urgente=false, Remoto=true, UsuarioId=4, ProblemaId=4, TecnicoId=4, Status=StatusChamado.EmAndamento},
        new Chamado{Titulo="Folha errada", Descricao="Valores inconsistentes", Urgente=true, Remoto=true, UsuarioId=5, ProblemaId=5},
        new Chamado{Titulo="Erro na folha", Descricao="Informações ausentes", Urgente=true, Remoto=true, UsuarioId=6, ProblemaId=6, TecnicoId=4, Status=StatusChamado.EmAndamento},
        new Chamado{Titulo="Erro orçamento", Descricao="Erro de digitação", Urgente=false, Remoto=true, UsuarioId=7, ProblemaId=7},
        new Chamado{Titulo="Pagamento sumiu", Descricao="Não consta nos registros", Urgente=true, Remoto=true, UsuarioId=8, ProblemaId=8},
        new Chamado{Titulo="Nota fiscal errada", Descricao="Erro de soma", Urgente=false, Remoto=false, UsuarioId=9, ProblemaId=9},
        new Chamado{Titulo="Sistema lento", Descricao="Financeiro demora a abrir", Urgente=false, Remoto=true, UsuarioId=10, ProblemaId=10}
            };
            
            context.Chamados.AddRange(chamados);
            context.SaveChanges();
        }
    }
}