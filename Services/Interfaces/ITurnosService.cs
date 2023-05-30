public interface ITurnosService
{
    IEnumerable<Turno> ObtenerTodosLosTurnos();
    Task<string> TerminarCita(TerminarCitaDto turno);

    Task<List<Turno>> ConsultarTurnos(ConsultarTurnosDto turno);

    Task<string> CancelarTurno(int IdTurno);

    Task<List<DateTime>> GetHorariosDisponibles(TurnosDisponiblesDto turnosDisponibles);


}