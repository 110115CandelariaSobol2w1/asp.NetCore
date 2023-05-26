using System.ComponentModel.DataAnnotations;

public class Mascotas{
    [Key]
    public int IdMascota{get;set;}
    public string Nombre{get;set;}
    public int IdCliente{get;set;}
    public string Tipo{get;set;}

    public Usuario Usuario{get;set;}


}