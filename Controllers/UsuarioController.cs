using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace mascotasApi.Net.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly AppDbContext context;

    public UsuarioController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<Usuario> Get()
    {
        return context.Usuarios.Where(u => u.dni != null && u.email != null && u.numero_tel != null).ToList();
    }

    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hash;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Usuario usuarioDto)
    {
        // Validar los datos recibidos
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Hashear la password
        string hashedPassword = HashPassword(usuarioDto.password);

        // Crear un nuevo usuario a partir del DTO recibido
        Usuario usuario = new Usuario
        {
            IdRol = usuarioDto.IdRol,
            dni = usuarioDto.dni,
            email = usuarioDto.email,
            numero_tel = usuarioDto.numero_tel,
            password = hashedPassword,
            username = usuarioDto.username
        };

        // Agregar el nuevo usuario al DbContext y guardar los cambios
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        // Retornar la respuesta HTTP 201 Created con los datos del usuario creado
        return CreatedAtAction(nameof(Get), new { id = usuario.IdUsuario }, usuario);
    }

    
}
