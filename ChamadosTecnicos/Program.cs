using ChamadosTecnicos.Data;
using ChamadosTecnicos.Models;
using ChamadosTecnicos.Routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options=>
{
    options.AddPolicy("dov",policy=>policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}); 

// Adicionar serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar Entity Framework com InMemoryDatabase
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ChamadosDb"));

var app = builder.Build();

// Configurar pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Inicializar dados de exemplo
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(context);
}

// Mapear endpoints minimalistas
app.MapChamadoEndpoints();
app.MapUsuarioEndpoints();
app.MapTecnicoEndpoints();
app.MapProblemasEndpoints();
// Mapear controllers tradicionais se ainda estiverem presentes
app.MapControllers();

// Endpoint utilitário para visualizar dados
app.MapGet("/ver-dados", async (AppDbContext dbContext) =>
{
    try
    {
        var dados = new
        {
            Departamentos = await dbContext.Departamentos.ToListAsync(),
            Problemas = await dbContext.Problemas.ToListAsync(),
            Usuarios = await dbContext.Usuarios.ToListAsync(),
            Tecnicos = await dbContext.Tecnicos.ToListAsync(),
            Chamados = await dbContext.Chamados.ToListAsync()
        };

        return Results.Ok(dados);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao acessar dados: {ex.Message}");
    }
});
app.UseCors("dov");
app.Run();
