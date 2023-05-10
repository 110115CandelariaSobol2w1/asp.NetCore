public interface IUserService
{

    IEnumerable<Usuario> Get();
    Task<Usuario> CreateUserAsync(usuarioDto createUserDto);
    Task<Usuario> Login(loginDto login);
    
    // Otros métodos que necesites
}