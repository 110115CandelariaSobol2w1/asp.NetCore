using System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class TurnosService : ITurnosService
{
    private readonly AppDbContext context;

    public TurnosService(AppDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Turno> ObtenerTodosLosTurnos()
    {
        return context.Turnos.ToList();
    }

    //8- terminar cita y cargar historial
    public async Task<string> TerminarCita(TerminarCitaDto turno)
    {

        const int IdEstado = 3;
        int IdTurno = turno.IdTurno;

        var queryBuilder = this.context.Turnos
        .Where(t => t.IdTurno == IdTurno)
        .FirstOrDefault();

        if (queryBuilder != null)
        {
            queryBuilder.IdEstado = IdEstado;

            await this.context.SaveChangesAsync();
        }

        var fecha = DateTime.Now;
        var descripcion = turno.descripcion;

        var historial = new Historial
        {
            IdTurno = IdTurno,
            fecha = fecha,
            descripcion = descripcion
        };

        this.context.Historial.Add(historial);

        await this.context.SaveChangesAsync();

        return "Turno finalizado. Historial cargado con éxito";

    }

    //7- ver mis citas psicologo
    public async Task<List<Turno>> ConsultarTurnos(ConsultarTurnosDto turno)
    {
        int IdPsicologo = turno.IdPsicologo;
        DateTime fecha = turno.fecha;

        var turnos = await this.context.Turnos
            .Where(t => t.IdPsicologo == IdPsicologo && t.Fecha_inicio.Date == fecha.Date)
            .ToListAsync();

        return turnos;
    }

    //5 cancelar turno

    public async Task<string> CancelarTurno(int IdTurno)
    {
        var turno = await this.context.Turnos.FirstOrDefaultAsync(t => t.IdTurno == IdTurno);

        if (turno != null)
        {
            turno.IdEstado = 3;
            await this.context.SaveChangesAsync();
            return "Turno cancelado con éxito";
        }
        else
        {
            return "No se encontró el turno especificado";
        }
    }

    //ver turnos disponibles 
    public async Task<List<DateTime>> GetHorariosDisponibles(TurnosDisponiblesDto turnosDisponibles)
    {
        // Obtener el tipo para ver el tiempo de la consulta
        var obtengoTipo = await context.Mascotas.FirstOrDefaultAsync(p => p.IdMascota == turnosDisponibles.IdMascota);
        var tipo = obtengoTipo?.Tipo;
        Console.WriteLine("Valor del tipo: " + tipo);

        if (tipo == null)
        {
            Console.WriteLine("Tipo de mascota no válido");
            return new List<DateTime>(); // Devolver una lista vacía si el tipo de mascota no es válido
        }

        var duracion = tipo == "gato" ? 45 : 30; // Duración según el tipo de mascota
        var fecha = turnosDisponibles.fecha;

        var fechaInicio = new DateTime(fecha.Year, fecha.Month, fecha.Day, 9, 0, 0); // Establecer la hora de inicio de la agenda
        var fechaFin = new DateTime(fecha.Year, fecha.Month, fecha.Day, 18, 0, 0); // Establecer la hora de fin de la agenda

        // Obtener los turnos programados para esa fecha
        var turnos = await context.Turnos.Where(t =>
            t.Fecha_inicio.Date == fechaInicio.Date &&
            t.IdPsicologo == turnosDisponibles.IdPsicologo).ToListAsync();

        var horariosDisponibles = new List<DateTime>(); // Crear lista para guardar los turnos disponibles
        var hora = fechaInicio;
        while (hora <= fechaFin)
        {
            // Verificar si la hora está disponible
            var horaFin = hora.AddMinutes(duracion);
            var disponible = await context.Turnos.CountAsync(t =>
                (t.Fecha_inicio <= horaFin && t.Fecha_fin >= hora) ||
                (t.Fecha_inicio <= hora && t.Fecha_fin >= horaFin));

            Console.WriteLine("DISPONIBILIDAD: " + disponible);

            if (disponible == 0)
            {
                horariosDisponibles.Add(hora);
            }
            // Avanzar a la siguiente hora
            hora = hora.AddMinutes(15); // Avanzar en bloques de 15 minutos
        }

        Console.WriteLine(horariosDisponibles);
        return horariosDisponibles;
    }



}
