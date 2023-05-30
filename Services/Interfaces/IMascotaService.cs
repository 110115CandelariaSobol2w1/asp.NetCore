using Microsoft.AspNetCore.Mvc;

public interface IMascotaService
{
    List<Mascotas> ObtenerMascotasConDuenos();

    Task<List<Mascotas>> ObtenerTurnosMascotas(int IdCliente);

    Task<List<Mascotas>> ObtenerTurnosHistorial(int IdMascota);

}