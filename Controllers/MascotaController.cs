using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[EnableCors("extrados")]
[ApiController]
[Route("[controller]")]

public class MascotaController : ControllerBase
{
    private readonly IMascotaService mascotaService;

    private IConfiguration config;

    public MascotaController(IMascotaService mascotaService, IConfiguration config)
    {
        this.mascotaService = mascotaService;
        this.config = config;
    }


    [HttpGet]
    public IActionResult ObtenerMascotasConDuenos()
    {
        var mascotasConDuenos = mascotaService.ObtenerMascotasConDuenos();

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            MaxDepth = 32 // Opcional: Puedes ajustar el valor de profundidad máxima según tus necesidades.
        };

        var jsonString = JsonSerializer.Serialize(mascotasConDuenos, options);

        return Ok(jsonString);
    }

    [HttpGet("obtenerTurnosMascotas/{IdCliente}")]
    public async Task<ActionResult<List<Mascotas>>> ObtenerTurnosMascotas(int IdCliente)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32 // Opcionalmente, puedes ajustar el valor máximo de profundidad si lo deseas
            };

            var turnosMascotas = await mascotaService.ObtenerTurnosMascotas(IdCliente);
            var jsonString = JsonSerializer.Serialize(turnosMascotas, options);
            return Content(jsonString, "application/json");
        }
        catch (Exception ex)
        {
            // Manejar errores y devolver una respuesta apropiada según sea necesario
            return StatusCode(500, "Error al obtener los turnos de las mascotas: " + ex.Message);
        }
    }

    [HttpGet("obtenerTurnosHistorial/{IdMascota}")]
    public async Task<ActionResult<List<Mascotas>>> ObtenerTurnosHistorial(int IdMascota)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32 // Opcionalmente, puedes ajustar el valor máximo de profundidad si lo deseas
            };

            var turnosHistorial = await mascotaService.ObtenerTurnosHistorial(IdMascota);
            var jsonString = JsonSerializer.Serialize(turnosHistorial, options);
            return Content(jsonString, "application/json");
        }
        catch (Exception ex)
        {
            // Manejar errores y devolver una respuesta apropiada según sea necesario
            return StatusCode(500, "Error al obtener los turnos con historial: " + ex.Message);
        }
    }



}