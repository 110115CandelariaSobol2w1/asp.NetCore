using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Historial
{
    [Key]
    public int IdHistoria { get; set; }
    public int IdTurno { get; set; }
    [ForeignKey("IdTurno")]

    public Turno Turno { get; set; }
    public DateTime fecha { get; set; }
    public string descripcion { get; set; }


}