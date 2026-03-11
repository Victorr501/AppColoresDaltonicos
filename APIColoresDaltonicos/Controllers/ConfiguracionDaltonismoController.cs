using APIColoresDaltonicos.Models.ConfiguracionDaltonismos.DTOs;
using APIColoresDaltonicos.Services.Excepcion;
using APIColoresDaltonicos.Services.Services.ConfiguracionDaltonismos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIColoresDaltonicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfiguracionDaltonismoController : ControllerBase
    {
        private readonly IConfiguracionDaltonismoService _configuracionDaltonismoService;

        public ConfiguracionDaltonismoController(IConfiguracionDaltonismoService configuracionDaltonismoService)
        {
            _configuracionDaltonismoService = configuracionDaltonismoService;
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerConfiguracionPorUsuarioId(int usuarioId)
        {
            try
            {
                var configuracion = await _configuracionDaltonismoService.ObtenerConfiguracionPorUsuarioIdAsync(usuarioId);
                return Ok(configuracion);
            }
            catch (UsuarioNoEncontradoException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPut("usuario/{usuarioId}")]
        public async Task<IActionResult> ActualizarConfiguracionUsuario(int usuarioId, [FromBody] ConfiguracionDaltonismoDto nuevaConfiguracion)
        {
            try
            {
                var configuracionActualizada = await _configuracionDaltonismoService.ActualizarConfiguracionUsuarioAsync(usuarioId, nuevaConfiguracion);
                return Ok(configuracionActualizada);
            }
            catch (UsuarioSinConfiguracionException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (TipoDaltonismoInvalidoException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
