using APIColoresDaltonicos.Models.ConfiguracionDaltonismos;
using APIColoresDaltonicos.Models.ConfiguracionDaltonismos.DTOs;
using APIColoresDaltonicos.Services.Services.Generic;
using APIColoresDaltonicos.Services.Excepcion;

using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Services.ConfiguracionDaltonismos
{
    public interface IConfiguracionDaltonismoService : IGenericService<ConfiguracionDaltonismo>
    {
        Task<ConfiguracionDaltonismo> ObtenerConfiguracionPorUsuarioIdAsync(int usuarioId);
        Task<ConfiguracionDaltonismo> ActualizarConfiguracionUsuarioAsync(int usuarioId, ConfiguracionDaltonismoDto nuevaConfiguracion);
    }
}
