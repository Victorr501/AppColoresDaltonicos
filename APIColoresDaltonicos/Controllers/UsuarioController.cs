using APIColoresDaltonicos.Models.Usuarios.DTOs;
using APIColoresDaltonicos.Services.Services.Usuarios;
using APIColoresDaltonicos.Models.Usuarios;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using APIColoresDaltonicos.Services.Excepcion;

namespace APIColoresDaltonicos.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroUsuarioDto peticion)
        {
            try
            {
                var nuevoUsuario = _mapper.Map<Usuario>(peticion);

                var resultadoDto = await _usuarioService.RegistrarUsuarioAsync(nuevoUsuario);

                return Ok(resultadoDto);
            }
            catch (EmailDuplicadoException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var resultadoDto = await _usuarioService.LoginAsync(login.Email, login.Password);

                return Ok(resultadoDto);
            }
            catch (CredencialesInvalidasException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioSeguro(int id) {
            try
            {
                var usuarioDto = await _usuarioService.ObtenerUsuarioSeguroPorIdAsync(id);
                return Ok(usuarioDto);
            }
            catch (UsuarioNoEncontradoException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPut("perfil")]
        public async Task<IActionResult> ActualizarPerfil([FromBody] UsuarioResponseDto usuarioActualizar)
        {
            try
            {
                var resultadoDto = await _usuarioService.ActualizarPerfilAsync(usuarioActualizar);
                return Ok(resultadoDto);
            }
            catch (UsuarioNoEncontradoException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (EmailDuplicadoException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> CambiarPassword(int id, [FromBody] CambiarPasswordDto passwords)
        {
            try
            {
                await _usuarioService.ActualizarPasswordAsync(id, passwords.PasswordActual, passwords.PasswordNueva);

                return NoContent();
            }
            catch (CredencialesInvalidasException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (UsuarioNoEncontradoException ex) 
            { 
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerPorIdAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                await _usuarioService.BorrarAsync(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el usuario con ID {Id}", id);
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
