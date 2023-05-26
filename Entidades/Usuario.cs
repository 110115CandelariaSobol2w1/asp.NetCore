using System.ComponentModel.DataAnnotations;

public class Usuario{

    [Key]
    public int IdUsuario{get;set;}
    public string username{get;set;}
    public string password{get;set;}
    public int IdRol{get;set;}
    public long? dni {get;set;}
    public string email {get;set;}
    public string numero_tel{get;set;}

    public List<Mascotas> Mascotas{get;set;}
}