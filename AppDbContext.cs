using System.Data.Common;
using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext {

    public DbSet<Usuario> Usuarios {get;set;}
    public DbSet<Mascotas> Mascotas{get;set;}

    public DbSet<Turno> Turnos{get;set;}

    public DbSet<Historial> Historial{get;set;}

public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
{
}

}