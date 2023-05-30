using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace mascotasApi.Net.Controllers;

[EnableCors("extrados")]
//[Authorize]
[ApiController]
[Route("[controller]")]

public class TurnoController : ControllerBase
{

    private readonly ITurnosService turnoService;
    private IConfiguration config;


    public TurnoController(ITurnosService turnoService, IConfiguration config)
    {
        this.turnoService = turnoService;
        this.config = config;
    }

    [HttpGet]
    public IActionResult ObtenerTodosLosTurnos()
    {
        var turnos = turnoService.ObtenerTodosLosTurnos();
        return Ok(turnos);
    }

    [HttpPost("terminarCita")]
    public async Task<ActionResult<string>> TerminarCita([FromBody] TerminarCitaDto turno)
    {
        try
        {
            var resultado = await turnoService.TerminarCita(turno);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            // Manejar errores y devolver una respuesta apropiada según sea necesario
            return StatusCode(500, "Error al terminar la cita: " + ex.Message);
        }
    }

    [HttpPost("consultarTurnos")]
    public async Task<ActionResult<List<Turno>>> ConsultarTurnos([FromBody] ConsultarTurnosDto turno)
    {
        try
        {
            var turnos = await turnoService.ConsultarTurnos(turno);
            return Ok(turnos);
        }
        catch (Exception ex)
        {
            // Manejar errores y devolver una respuesta apropiada según sea necesario
            return StatusCode(500, "Error al consultar los turnos: " + ex.Message);
        }
    }



    [HttpPost("cancelarTurno/{IdTurno}")]
    public async Task<ActionResult<string>> CancelarTurno(int IdTurno)
    {
        try
        {
            var resultado = await turnoService.CancelarTurno(IdTurno);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            // Manejar errores y devolver una respuesta apropiada según sea necesario
            return StatusCode(500, "Error al cancelar el turno: " + ex.Message);
        }
    }

    [HttpPost("horarios-disponibles")]
    public async Task<ActionResult<List<DateTime>>> GetHorariosDisponibles(TurnosDisponiblesDto turnosDisponibles)
    {
        try
        {
            var horariosDisponibles = await turnoService.GetHorariosDisponibles(turnosDisponibles);
            return Ok(horariosDisponibles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener los horarios disponibles: {ex.Message}");
        }
    }



}