using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly AppDbContext context;

    public UserService(AppDbContext context)
    {
        this.context = context;
    }

    public UserService()
    {

    }

    public IEnumerable<Usuario> Get()
    {
        return context.Usuarios.Where(u => u.dni != null && u.email != null && u.numero_tel != null).ToList();
    }

    public IEnumerable<psicologoDto> GetPsicologos()
    {
         return context.Usuarios
        .Where(u => u.IdRol == 3)
        .Select(u => new psicologoDto { IdRol = u.IdRol, username = u.username })
        .ToList();
    }

    public static string HashPassword(string password)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);
        return Convert.ToBase64String(hashedPassword);
    }

    public async Task<Usuario> CreateUserAsync(usuarioDto createUserDto)

    {
        // Hashear la contraseña
        string hashedPassword = HashPassword(createUserDto.password);

        var newUser = new Usuario();

        newUser.username = createUserDto.username;
        newUser.password = hashedPassword;
        newUser.IdRol = createUserDto.IdRol;
        newUser.dni = createUserDto.dni;
        newUser.email = createUserDto.email;
        newUser.numero_tel = createUserDto.numero_tel;

        // Agregar el nuevo usuario al DbContext y guardar los cambios
        context.Usuarios.Add(newUser);
        await context.SaveChangesAsync();

        return newUser;
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedInput = sha.ComputeHash(asByteArray);
        var hashedInputString = Convert.ToBase64String(hashedInput);

        return hashedInputString == hashedPassword;
    }

    public async Task<Usuario> Login(loginDto userLogin)
    {
        // Buscar el usuario por nombre de usuario
        var user = await context.Usuarios.SingleOrDefaultAsync(x => x.username == userLogin.username);

        // Verificar si se encontró un usuario y si la contraseña coincide
        if (user != null && VerifyPassword(userLogin.password, user.password))
        {
            return user; // El login es exitoso, devolver el usuario
        }

        return null; // El login ha fallado
    }

    public async Task<Usuario> ObtenerUsuariosConMascotas(int IdUsuario)
    {
        var usuario = await context.Usuarios
            .Include(u => u.Mascotas) // Incluye las mascotas del usuario
            .FirstOrDefaultAsync(u => u.IdUsuario == IdUsuario);

        return usuario;
    }

}