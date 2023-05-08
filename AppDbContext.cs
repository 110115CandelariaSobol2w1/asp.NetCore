using System.Data.Common;
using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext {

    public DbSet<Usuario> Usuarios {get;set;}

public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
{
}

}