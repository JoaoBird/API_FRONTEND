using ChamadosTecnicos.Data;
using ChamadosTecnicos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChamadosTecnicos.Routes
{
    public static class TecnicosRoutes
    {
        public static void MapTecnicoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/tecnicos");

            group.MapGet("/", async (AppDbContext db) =>
            {
                var tecnicos = await db.Tecnicos.ToListAsync();
                return Results.Ok(tecnicos);
            });

            group.MapGet("/{id}", async (int id, AppDbContext db) =>
            {
                var tecnico = await db.Tecnicos.FindAsync(id);
                return tecnico is null ? Results.NotFound() : Results.Ok(tecnico);
            });

            group.MapGet("/disponiveis", async (AppDbContext db) =>
            {
                var tecnicos = await db.Tecnicos
                    .Where(t => db.Chamados.Count(c => c.TecnicoId == t.Id && c.Status != StatusChamado.Fechado) < 5)
                    .ToListAsync();
                return Results.Ok(tecnicos);
            });

            group.MapGet("/{id}/chamados", async (int id, AppDbContext db) =>
            {
                var tecnico = await db.Tecnicos.FindAsync(id);
                if (tecnico == null) return Results.NotFound("Técnico não encontrado");

                var chamados = await db.Chamados
                    .Where(c => c.TecnicoId == id)
                    .Include(c => c.Usuario)
                    .Include(c => c.Problema)
                    .ToListAsync();

                return Results.Ok(chamados);
            });

            group.MapGet("/{id}/chamados/abertos", async (int id, AppDbContext db) =>
            {
                var tecnico = await db.Tecnicos.FindAsync(id);
                if (tecnico == null) return Results.NotFound("Técnico não encontrado");

                var chamados = await db.Chamados
                    .Where(c => c.TecnicoId == id && c.Status != StatusChamado.Fechado)
                    .Include(c => c.Usuario)
                    .Include(c => c.Problema)
                    .ToListAsync();

                return Results.Ok(chamados);
            });
        }
    }
}
