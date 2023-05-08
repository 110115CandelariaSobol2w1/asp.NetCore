using Microsoft.AspNetCore.Mvc;

namespace mascotasApi.Net.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly AppDbContext context;

    public UsuarioController(AppDbContext context){
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<Usuario> Get(){
        return context.Usuarios.Where(u => u.dni != null && u.email != null && u.numero_tel != null).ToList();
    }
}
