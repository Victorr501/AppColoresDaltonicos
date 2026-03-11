using APIColoresDaltonicos.Repositories.Repositories.Usuarios;
using APIColoresDaltonicos.Services.Services.Generic;
using APIColoresDaltonicos.Models.Usuarios.DTOs;
using APIColoresDaltonicos.Models.Usuarios;
using Microsoft.Extensions.Logging;
using APIColoresDaltonicos.Services.Excepcion;
using AutoMapper;
using APIColoresDaltonicos.Services.Encriptar;
using APIColoresDaltonicos.Models.Auth.DTOs;
using APIColoresDaltonicos.Services.Token;
using APIColoresDaltonicos.Repositories.Repositories.ConfiguracionDaltonismos;

namespace APIColoresDaltonicos.Services.Services.Usuarios
{
    public class UsuarioService : GenericService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IEncriptacionService _encriptacionService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguracionDaltonismoRepository _configuracionDaltonismoRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger, IEncriptacionService encriptacionService, ITokenService tokenService,IConfiguracionDaltonismoRepository configuracionDaltonismoRepository, IMapper mapper) : base(usuarioRepository, logger)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _encriptacionService = encriptacionService;
            _tokenService = tokenService;
            _configuracionDaltonismoRepository = configuracionDaltonismoRepository;
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

        public async Task<AuthResponseDto> RegistrarUsuarioAsync(Usuario nuevoUsuario)
        {
            _logger.LogInformation("Registrando nuevo usuario con email: {Email}", nuevoUsuario.Email);
            var usuarioExistente = await _usuarioRepository.ObtenerPorEmailAsync(nuevoUsuario.Email);

            if (usuarioExistente != null)
            {
                _logger.LogWarning("Registro fallido: El email {Email} ya esta en uso", nuevoUsuario.Email);
                throw new CredencialesInvalidasException("El email ya esta en uso");
            }

            
            nuevoUsuario.PasswordHash = _encriptacionService.HashPassword(nuevoUsuario.PasswordHash);

            _logger.LogInformation("Registro nuevo usuario...");
            await base.AñadirAsync(nuevoUsuario);

            var usuarioLimpio = _mapper.Map<UsuarioResponseDto>(nuevoUsuario);
            var token = _tokenService.CrearToken(nuevoUsuario);

            return new AuthResponseDto { Usuario = usuarioLimpio, Token = token };
        }

        public async Task<AuthResponseDto> LoginAsync(string email, string password)
        {
            _logger.LogInformation("Intento de login para email: {email}", email);

            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);

            if (usuario == null)
            {
                _logger.LogWarning("Login fallido: No se encontró el email: {email}", email);
                throw new CredencialesInvalidasException("El correo o la contraseña son incorrectos");
            }

            
            var coinciden = _encriptacionService.VerifyPassword(password, usuario.PasswordHash);

            if (!coinciden)
            {
                _logger.LogWarning("Login fallido: No se encontró el email: {email}", email);
                throw new CredencialesInvalidasException("El correo o la contraseña son incorrectos");
            }

            _logger.LogInformation("Login exitoso para el usuario {email}", email);
            var usuarioLimpio = _mapper.Map<UsuarioResponseDto>(usuario);

            var token = _tokenService.CrearToken(usuario);

            return new AuthResponseDto {Usuario = usuarioLimpio, Token = token};
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

            
            var contraseñaCorrecta = _encriptacionService.VerifyPassword(password, usuario.PasswordHash);
            if (!contraseñaCorrecta)
            {
                _logger.LogWarning("Actualización de contraseña fallida: Contraseña actual incorrecta para el usuario ID: {Id}", id);
                throw new CredencialesInvalidasException("La contraseña actual es incorrecta");
            }

            
            usuario.PasswordHash = _encriptacionService.HashPassword(passwordNueva);

            await base.ActualizarAsync(usuario);
            _logger.LogInformation("Contraseña actualizada exitosamente para el usuario ID: {Id}", id);
        }

        public async Task BorrarAsync(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
            if(usuario == null)
            {
                _logger.LogWarning("Usuario con id: {id} no encontrado", id);
                throw new UsuarioNoEncontradoException($"Usuario con id: {id} no encontrado");
            }

            var configuracion = await _configuracionDaltonismoRepository.ObtenerConfiguracionPorUsuarioIdAsync(id);

            if (configuracion != null)
            {
                await _configuracionDaltonismoRepository.BorrarAsync(configuracion);
            }

            await _usuarioRepository.BorrarAsync(usuario);

            await _usuarioRepository.GuardarCambiosAsync();
        }
    }
}
