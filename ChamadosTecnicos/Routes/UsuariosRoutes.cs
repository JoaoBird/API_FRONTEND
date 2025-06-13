using ChamadosTecnicos.Data;
using ChamadosTecnicos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChamadosTecnicos.Routes
{
    public static class UsuariosRoutes
    {
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/usuarios");

            group.MapGet("/", async (AppDbContext db) =>
            {
                var usuarios = await db.Usuarios.ToListAsync();
                return Results.Ok(usuarios);
            });

            group.MapGet("/{id}", async (int id, AppDbContext db) =>
            {
                var usuario = await db.Usuarios.FindAsync(id);
                return usuario is null ? Results.NotFound() : Results.Ok(usuario);
            });

            group.MapPost("/", async (Usuario usuario, AppDbContext db) =>
            {
                var existe = await db.Usuarios.AnyAsync(u => u.Email == usuario.Email);
                if (existe) return Results.BadRequest("Já existe um usuário com este email");

                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
            });

            group.MapGet("/{id}/chamados", async (int id, AppDbContext db) =>
            {
                var usuario = await db.Usuarios.FindAsync(id);
                if (usuario == null) return Results.NotFound("Usuário não encontrado");

                var chamados = await db.Chamados
                    .Where(c => c.UsuarioId == id)
                    .Include(c => c.Problema)
                    .Include(c => c.Tecnico)
                    .ToListAsync();

                return Results.Ok(chamados);
            });
        }
    }
}
