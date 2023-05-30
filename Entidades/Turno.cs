using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Turno{
    [Key]
    public int IdTurno{get;set;}
    public int IdMascota{get;set;}
    [ForeignKey("IdMascota")]
    public Mascotas Mascota { get; set; }
    public DateTime Fecha_inicio{get;set;}
    public DateTime Fecha_fin{get;set;}
    public int IdEstado{get;set;}
    public int IdPsicologo{get;set;}

    public Historial Historial { get; set; }

    
    

}