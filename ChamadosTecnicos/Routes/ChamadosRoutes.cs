using ChamadosTecnicos.Data;
using ChamadosTecnicos.DTOs;
using ChamadosTecnicos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChamadosTecnicos.Routes
{
    public static class ChamadosRoutes
    {
        public static void MapChamadoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/chamados");

            group.MapGet("/", async (AppDbContext db) =>
            {
                var chamados = await db.Chamados
                    .Include(c => c.Usuario)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Problema)
                    .ThenInclude(p => p.Departamento)
                    .Select(c => new ChamadoDTO
                    {
                        Id = c.Id,
                        Titulo = c.Titulo,
                        Descricao = c.Descricao,
                        DataAbertura = c.DataAbertura,
                        DataFechamento = c.DataFechamento,
                        Urgente = c.Urgente,
                        Remoto = c.Remoto,
                        Status = c.Status.ToString(),
                        NotaAtendimento = c.NotaAtendimento,
                        TempoGasto = c.TempoGasto,
                        UsuarioNome = c.Usuario.Nome,
                        TecnicoNome = c.Tecnico != null ? c.Tecnico.Nome : null,
                        ProblemaNome = c.Problema.Nome,
                        DepartamentoNome = c.Problema.Departamento.Nome
                    })
                    .ToListAsync();
                return Results.Ok(chamados);
            });

            group.MapGet("/{id}", async (int id, AppDbContext db) =>
            {
                var chamado = await db.Chamados
                    .Include(c => c.Usuario)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Problema)
                    .ThenInclude(p => p.Departamento)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (chamado == null) return Results.NotFound();

                var dto = new ChamadoDTO
                {
                    Id = chamado.Id,
                    Titulo = chamado.Titulo,
                    Descricao = chamado.Descricao,
                    DataAbertura = chamado.DataAbertura,
                    DataFechamento = chamado.DataFechamento,
                    Urgente = chamado.Urgente,
                    Remoto = chamado.Remoto,
                    Status = chamado.Status.ToString(),
                    NotaAtendimento = chamado.NotaAtendimento,
                    TempoGasto = chamado.TempoGasto,
                    UsuarioNome = chamado.Usuario.Nome,
                    TecnicoNome = chamado.Tecnico?.Nome,
                    ProblemaNome = chamado.Problema.Nome,
                    DepartamentoNome = chamado.Problema.Departamento.Nome
                };
                return Results.Ok(dto);
            });

            group.MapPost("/", async (ChamadoCreateDTO dto, AppDbContext db) =>
            {
                var usuario = await db.Usuarios.FindAsync(dto.UsuarioId);
                var problema = await db.Problemas.FindAsync(dto.ProblemaId);
                if (usuario == null || problema == null) return Results.BadRequest("Usuário ou Problema inválido");

                var chamado = new Chamado
                {
                    Titulo = dto.Titulo,
                    Descricao = dto.Descricao,
                    Urgente = dto.Urgente,
                    Remoto = dto.Remoto,
                    UsuarioId = dto.UsuarioId,
                    ProblemaId = dto.ProblemaId,
                    Status = StatusChamado.Aberto,
                    DataAbertura = DateTime.Now
                };

                db.Chamados.Add(chamado);
                await db.SaveChangesAsync();

                return Results.Created($"/api/chamados/{chamado.Id}", chamado);
            });

            group.MapDelete("/{id:int}/delete", async (int id, AppDbContext db) =>
            {
                var chamado = await db.Chamados.FindAsync(id);
                if (chamado == null) return Results.NotFound();

                db.Chamados.Remove(chamado);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapPut("/{id}/atender", async (int id, AtenderChamadoDTO dto, AppDbContext db) =>
            {
                var chamado = await db.Chamados.FindAsync(id);
                if (chamado == null) return Results.NotFound();

                var tecnico = await db.Tecnicos.FindAsync(dto.TecnicoId);
                if (tecnico == null) return Results.BadRequest("Técnico não encontrado");

                if (chamado.Status != StatusChamado.Aberto) return Results.BadRequest("Chamado deve estar 'Aberto'");

                chamado.TecnicoId = dto.TecnicoId;
                chamado.Status = StatusChamado.EmAndamento;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapPut("/{id}/resolver", async (int id, ChamadoUpdateDTO dto, AppDbContext db) =>
            {
                var chamado = await db.Chamados.FindAsync(id);
                if (chamado == null) return Results.NotFound();

                if (chamado.Status != StatusChamado.EmAndamento) return Results.BadRequest("Chamado deve estar 'Em Andamento'");

                chamado.NotaAtendimento = dto.NotaAtendimento;
                chamado.TempoGasto = dto.TempoGasto;
                chamado.Status = StatusChamado.Resolvido;
                chamado.DataFechamento = DateTime.Now;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapPut("/{id}/fechar", async (int id, AppDbContext db) =>
            {
                var chamado = await db.Chamados.FindAsync(id);
                if (chamado == null) return Results.NotFound();

                if (chamado.Status != StatusChamado.Resolvido) return Results.BadRequest("Chamado deve estar 'Resolvido'");

                chamado.Status = StatusChamado.Fechado;
                chamado.DataFechamento ??= DateTime.Now;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
