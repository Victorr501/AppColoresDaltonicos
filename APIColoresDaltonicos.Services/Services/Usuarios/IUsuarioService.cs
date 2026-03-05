using APIColoresDaltonicos.Models.Usuarios;
using APIColoresDaltonicos.Models.Usuarios.DTOs;
using APIColoresDaltonicos.Services.Services.Generic;

namespace APIColoresDaltonicos.Services.Services.Usuarios
{
    public interface IUsuarioService : IGenericService<Usuario>
    {
        Task<UsuarioResponseDto> RegistrarUsuarioAsync(Usuario nuevoUsuario);
        Task<UsuarioResponseDto> LoginAsync(string username, string password);
        Task<UsuarioResponseDto> ObtenerUsuarioSeguroPorIdAsync(int id);
        Task<UsuarioResponseDto> ActualizarPerfilAsync(UsuarioResponseDto usaurioActualizar);
        Task ActualizarPasswordAsync(int id, string password, string passwordNueva);
    }
}
