using APIColoresDaltonicos.Services.Services.Usuarios;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIColoresDaltonicos.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
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
