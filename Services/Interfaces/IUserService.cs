public interface IUserService
{

    IEnumerable<Usuario> Get();

    IEnumerable<psicologoDto> GetPsicologos();
    Task<Usuario> CreateUserAsync(usuarioDto createUserDto);
    Task<Usuario> Login(loginDto login);
    
    
}