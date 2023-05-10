using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using AutoMapper;

namespace mascotasApi.Net.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public UsuarioController(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<Usuario> Get()
    {
        return context.Usuarios.Where(u => u.dni != null && u.email != null && u.numero_tel != null).ToList();
    }

    public static string HashPassword(string password)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);
        return Convert.ToBase64String(hashedPassword);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] usuarioDto usuarioDto)
    {

        // Hashear la password
        string hashedPassword = HashPassword(usuarioDto.password);

        // Crear un nuevo usuario a partir del DTO recibido
        var usuario = mapper.Map<Usuario>(usuarioDto);
        usuario.password = hashedPassword;

        // Agregar el nuevo usuario al DbContext y guardar los cambios
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        return Ok();
    }

    
}
