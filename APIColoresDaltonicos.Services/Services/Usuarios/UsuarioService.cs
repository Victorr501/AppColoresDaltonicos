using APIColoresDaltonicos.Repositories.Repositories.Usuarios;
using APIColoresDaltonicos.Services.Services.Generic;
using APIColoresDaltonicos.Models.Usuarios.DTOs;
using APIColoresDaltonicos.Models.Usuarios;
using Microsoft.Extensions.Logging;
using APIColoresDaltonicos.Services.Excepcion;
using AutoMapper;

namespace APIColoresDaltonicos.Services.Services.Usuarios
{
    public class UsuarioService : GenericService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger, IMapper mapper) : base(usuarioRepository, logger)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioResponseDto> ObtenerUsuarioSeguroPorIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo usuario seguro por ID: {Id}", id);
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);

            if (usuario == null)
            {
                _logger.LogWarning("Usuario no encontrado con ID: {Id}", id);
                throw new UsuarioNoEncontradoException("Usuario no encontrado");
            }

            return _mapper.Map<UsuarioResponseDto>(usuario);
        }

        public async Task<UsuarioResponseDto> RegistrarUsuarioAsync(Usuario nuevoUsuario)
        {
            _logger.LogInformation("Registrando nuevo usuario con email: {Email}", nuevoUsuario.Email);
            var usuarioExistente = await _usuarioRepository.ObtenerPorEmailAsync(nuevoUsuario.Email);

            if (usuarioExistente != null)
            {
                _logger.LogWarning("Registro fallido: El email {Email} ya esta en uso", nuevoUsuario.Email);
                throw new CredencialesInvalidasException("El email ya esta en uso");
            }

            // Aqui se ecriptara la contraseña antes de guardarla
            //    
            // =======================================

            _logger.LogInformation("Registro nuevo usuario...");
            await base.AñadirAsync(nuevoUsuario);

            return _mapper.Map<UsuarioResponseDto>(nuevoUsuario);
        }

        public async Task<UsuarioResponseDto> LoginAsync(string email, string password)
        {
            _logger.LogInformation("Intento de login para email: {email}", email);

            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);

            if (usuario == null)
            {
                _logger.LogWarning("Login fallido: No se encontró el email: {email}", email);
                throw new CredencialesInvalidasException("El correo o la contraseña son incorrectos");
            }

            // Comprobar si las credenciales coinciden
            var coinciden = true;

            if (!coinciden)
            {
                _logger.LogWarning("Login fallido: No se encontró el email: {email}", email);
                throw new CredencialesInvalidasException("El correo o la contraseña son incorrectos");
            }

            _logger.LogInformation("Login exitoso para el usuario {email}", email);
            return _mapper.Map<UsuarioResponseDto>(usuario);
        }

        public async Task<UsuarioResponseDto> ActualizarPerfilAsync(UsuarioResponseDto actualizarUsuario)
        {
            _logger.LogInformation("Actualizando perfil para el usuario ID: {Id}", actualizarUsuario.Id);
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(actualizarUsuario.Id);

            if (usuario == null)
            {
                _logger.LogWarning("Actualización fallida: Usuario no encontrado con ID: {Id}", actualizarUsuario.Id);
                throw new UsuarioNoEncontradoException("Usuario no encontrado");
            }

            if (usuario.Email != actualizarUsuario.Email)
            {
                var emailEnUso = await _usuarioRepository.ObtenerPorEmailAsync(actualizarUsuario.Email);
                if (emailEnUso != null && emailEnUso.Id != actualizarUsuario.Id)
                {
                    _logger.LogWarning("Actualización fallida: El email {Email} ya esta en uso", actualizarUsuario.Email);
                    throw new EmailDuplicadoException("El email ya esta en uso");
                }
            }

            usuario.Name = actualizarUsuario.Name;
            usuario.Email = actualizarUsuario.Email;

            await base.ActualizarAsync(usuario);
            _logger.LogInformation("Perfil actualizado exitosamente para el usuario ID: {Id}", actualizarUsuario.Id);
            return _mapper.Map<UsuarioResponseDto>(usuario);
        }

        public async Task ActualizarPasswordAsync(int id, string password, string passwordNueva)
        {
            _logger.LogInformation("Actualizando contraseña para el usuario ID: {Id}", id);
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("Actualización de contraseña fallida: Usuario no encontrado con ID: {Id}", id);
                throw new UsuarioNoEncontradoException("Usuario no encontrado");
            }

            // Comprobar si la contraseña actual es correcta
            var contraseñaCorrecta = true;
            if (!contraseñaCorrecta)
            {
                _logger.LogWarning("Actualización de contraseña fallida: Contraseña actual incorrecta para el usuario ID: {Id}", id);
                throw new CredencialesInvalidasException("La contraseña actual es incorrecta");
            }

            // Aqui se ecriptara la nueva contraseña antes de guardarla


            await base.ActualizarAsync(usuario);
            _logger.LogInformation("Contraseña actualizada exitosamente para el usuario ID: {Id}", id);
        }
    }
}
