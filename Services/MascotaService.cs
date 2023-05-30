using Microsoft.EntityFrameworkCore;

public class MascotaService : IMascotaService
{
    private readonly AppDbContext context;

    public MascotaService(AppDbContext context)
    {
        this.context = context;
    }

    public List<Mascotas> ObtenerMascotasConDuenos()
    {
        return context.Mascotas
            .Include(m => m.Usuario) // Carga la entidad de Usuario asociada a cada Mascota
            .ToList();
    }

    //4 ver mis turnos cliente

    public async Task<List<Mascotas>> ObtenerTurnosMascotas(int IdCliente)
    {
        var turnosMascotas = await this.context.Mascotas
            .Include(p => p.Turnos)
            .Where(p => p.IdCliente == IdCliente && p.Turnos.Any(t => t.IdEstado == 1))
            .ToListAsync();

        return turnosMascotas;
    }

    //6 ver info de la mascota
    public async Task<List<Mascotas>> ObtenerTurnosHistorial(int IdMascota)
{
    var turnosHistorial = await this.context.Mascotas
        .Include(p => p.Turnos)
            .ThenInclude(t => t.Historial)
        .Where(p => p.IdMascota == IdMascota)
        .ToListAsync();

    return turnosHistorial;
}

}