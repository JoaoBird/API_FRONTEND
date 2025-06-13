using ChamadosTecnicos.Data;
using ChamadosTecnicos.DTOs;
using ChamadosTecnicos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChamadosTecnicos.Routes
{
    public static class ProblemasRoutes
    {
        public static void MapProblemasEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/problemas");

            group.MapGet("/", async (AppDbContext db) =>
            {
                var problemas = await db.Problemas
                    .ToListAsync();
                return Results.Ok(problemas);
            });
        }
    }
}
