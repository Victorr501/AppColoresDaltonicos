using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos.DTOs;
using APIColoresDaltonicos.Repositories.Repositories.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Services.Services.Generic;
using Microsoft.Extensions.Logging;
using APIColoresDaltonicos.Services.Excepcion;
using AutoMapper;

namespace APIColoresDaltonicos.Services.Services.ConfiguracionDaltonismos
{
    public class ConfiguracionDaltonismoService : GenericService<ConfiguracionDaltonismo>, IConfiguracionDaltonismoService
    {
        private readonly IConfiguracionDaltonismoRepository _configuracionDaltonismoRepository;
        private readonly IMapper _mapper;
        private readonly List<string> _tiposValidos = new List<string> { "protanopia", "deuteranopia", "tritanopia", "acromatopsia", "ninguno" };

        public ConfiguracionDaltonismoService(IConfiguracionDaltonismoRepository configuracionDaltonismoRepository,IMapper mapper, ILogger<ConfiguracionDaltonismoService> logger) : base(configuracionDaltonismoRepository, logger)
        {
            _configuracionDaltonismoRepository = configuracionDaltonismoRepository;
            _mapper = mapper;
        }

        public async Task<ConfiguracionDaltonismo> ObtenerConfiguracionPorUsuarioIdAsync(int usuarioId)
        {
            _logger.LogInformation("Obteniendo configuración de daltonismo para usuario ID: {UsuarioId}", usuarioId);
            var configuracion = await _configuracionDaltonismoRepository.ObtenerConfiguracionPorUsuarioIdAsync(usuarioId);

            if (configuracion == null)
            {
                throw new UsuarioSinConfiguracionException($"No se encontró configuración de daltonismo para el usuario con ID {usuarioId}");
            }

            return configuracion;
        }

        public async Task<ConfiguracionDaltonismo> ActualizarConfiguracionUsuarioAsync(int usuarioId, ConfiguracionDaltonismoDto nuevaConfiguracion)
        {
            _logger.LogInformation("Actualizando configuración de daltonismo para usuario ID: {UsuarioId}", usuarioId);
            string tipoFormateado = nuevaConfiguracion.TipoDaltonismo?.ToLower() ?? "";
            if (tipoFormateado == "tricromacia")
            {
                tipoFormateado = "tritanopia";
            }
            if (!_tiposValidos.Contains(tipoFormateado))
            {
                _logger.LogWarning("Tipo de daltonismo inválido: {TipoDaltonismo}", nuevaConfiguracion.TipoDaltonismo);
                throw new TipoDaltonismoInvalidoException($"El tipo de daltonismo '{nuevaConfiguracion.TipoDaltonismo}' no es válido. Tipos válidos: {string.Join(", ", _tiposValidos)}");
            }
            nuevaConfiguracion.TipoDaltonismo = tipoFormateado;

            var configuracionExistente = await _configuracionDaltonismoRepository.ObtenerConfiguracionPorUsuarioIdAsync(usuarioId);
            if (configuracionExistente == null)
            {
                _logger.LogWarning("No se encontró configuración existente para el usuario ID: {UsuarioId}", usuarioId);
                throw new UsuarioSinConfiguracionException($"No se encontró configuración de daltonismo para el usuario con ID {usuarioId}");
            }

            _mapper.Map(nuevaConfiguracion, configuracionExistente);
            await _configuracionDaltonismoRepository.ActualizarAsync(configuracionExistente);
            await _configuracionDaltonismoRepository.GuardarCambiosAsync();

            return configuracionExistente;

        }
    }
}
